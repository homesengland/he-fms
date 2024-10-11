using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
using HE.FMS.Middleware.BusinessLogic.Tests.Factories;
using HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
using HE.FMS.Middleware.Claims.Functions;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Providers.Common;
using HE.FMS.Middleware.Providers.Common.Settings;
using HE.FMS.Middleware.Providers.File;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NSubstitute;
using Xunit;
using static HE.FMS.Middleware.Common.Constants;

namespace HE.FMS.Middleware.Claims.Tests.Functions;
public class ProcessAndStoreClaimTimeTriggerTests
{
    private readonly IEfinCosmosClient _efinCosmosClient;
    private readonly IEfinIndexCosmosClient _efinIndexCosmosClient;
    private readonly ClaimConverter _claimConverter;
    private readonly IFileWriter _fileWriter;

    private readonly ProcessAndStoreClaimTimeTrigger _function;

    public ProcessAndStoreClaimTimeTriggerTests()
    {
        var allowedEnvironmentSettings = new AllowedEnvironmentSettings("test");

        var logger = Substitute.For<ILogger<ProcessAndStoreClaimTimeTrigger>>();
        var environmentValidator = new EnvironmentValidator(allowedEnvironmentSettings);

        _claimConverter = new ClaimConverter(new FakeDateTimeProvider(), new FakeEfinLookupService());
        var csvGenerator = new CsvFileGenerator(new FakeDateTimeProvider());

        _efinCosmosClient = Substitute.For<IEfinCosmosClient>();
        _efinIndexCosmosClient = Substitute.For<IEfinIndexCosmosClient>();

        _fileWriter = Substitute.For<IFileWriter>();

        _function = new ProcessAndStoreClaimTimeTrigger(
            _efinCosmosClient,
            _fileWriter,
            csvGenerator,
            _efinIndexCosmosClient,
            _claimConverter,
            environmentValidator,
            logger);
    }

    [Fact]
    public async Task Run_ShouldProcessAndStoreClaim()
    {
        // Arrange
        var idempotencyKey = Guid.NewGuid().ToString();

        var requests = new List<ClaimPaymentRequest>
        {
            PaymentRequestFactory.CreateRandomClaimPaymentRequest(),
            PaymentRequestFactory.CreateRandomClaimPaymentRequest(),
        };
        var claimItems = requests.Select(async x => await _claimConverter.CreateItems(x)).Select(x => x.Result).ToList();
        var cosmosItems = claimItems.Select(x => EfinItem.CreateEfinItem(CosmosDbConfiguration.PartitonKey, JObject.FromObject(x), idempotencyKey, "test", CosmosDbItemType.Claim));

        _efinCosmosClient.GetAllNewItemsAsync(CosmosDbItemType.Claim, "test").Returns(cosmosItems);

        var index = EfinIndexItem.Create(
            CosmosDbConfiguration.PartitonKey,
            CosmosDbItemType.Claim,
            IndexConfiguration.Claim.BatchIndex,
            IndexConfiguration.Claim.BatchIndexLength,
            IndexConfiguration.Claim.BatchIndexPrefix);

        _efinIndexCosmosClient.GetNextIndex(IndexConfiguration.Claim.BatchIndex, CosmosDbItemType.Claim).Returns(index);

        // Act
        await _function.Run(new TimerInfo(), CancellationToken.None);

        // Assert
        await _efinCosmosClient.Received(1).GetAllNewItemsAsync(CosmosDbItemType.Claim, "test");
        await _efinIndexCosmosClient.Received(1).GetNextIndex(IndexConfiguration.Claim.BatchIndex, CosmosDbItemType.Claim);
        await _fileWriter.Received(3).WriteAsync(Arg.Any<string>(), Arg.Any<BlobData>());
    }
}
