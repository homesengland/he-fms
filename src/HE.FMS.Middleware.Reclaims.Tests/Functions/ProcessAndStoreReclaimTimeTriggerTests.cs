using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
using HE.FMS.Middleware.BusinessLogic.Tests.Factories;
using HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Providers.Common;
using HE.FMS.Middleware.Providers.Common.Settings;
using HE.FMS.Middleware.Providers.File;
using HE.FMS.Middleware.Reclaims.Functions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NSubstitute;
using Xunit;
using static HE.FMS.Middleware.Common.Constants;

namespace HE.FMS.Middleware.Reclaims.Tests.Functions;
public class ProcessAndStoreReclaimTimeTriggerTests
{
    private readonly IEfinCosmosClient _efinCosmosClient;
    private readonly IEfinIndexCosmosClient _efinIndexCosmosClient;
    private readonly ReclaimConverter _reclaimConverter;
    private readonly IFileWriter _fileWriter;

    private readonly ProcessAndStoreReclaimTimeTrigger _function;

    public ProcessAndStoreReclaimTimeTriggerTests()
    {
        var allowedEnvironmentSettings = new AllowedEnvironmentSettings("test");

        var logger = Substitute.For<ILogger<ProcessAndStoreReclaimTimeTrigger>>();
        var environmentValidator = new EnvironmentValidator(allowedEnvironmentSettings);

        _reclaimConverter = new ReclaimConverter(new FakeDateTimeProvider(), new FakeEfinLookupService());
        var csvGenerator = new CsvFileGenerator(new FakeDateTimeProvider());

        _efinCosmosClient = Substitute.For<IEfinCosmosClient>();
        _efinIndexCosmosClient = Substitute.For<IEfinIndexCosmosClient>();

        _fileWriter = Substitute.For<IFileWriter>();

        _function = new ProcessAndStoreReclaimTimeTrigger(
            _efinCosmosClient,
            _fileWriter,
            csvGenerator,
            _efinIndexCosmosClient,
            _reclaimConverter,
            environmentValidator,
            logger);
    }

    [Fact]
    public async Task Run_ShouldProcessAndStoreClaim()
    {
        // Arrange
        var idempotencyKey = Guid.NewGuid().ToString();

        var requests = new List<ReclaimPaymentRequest>
        {
            PaymentRequestFactory.CreateRandomReclaimPaymentRequest(),
            PaymentRequestFactory.CreateRandomReclaimPaymentRequest(),
        };
        var reclaimItems = requests.Select(async x => await _reclaimConverter.CreateItems(x)).Select(x => x.Result).ToList();
        var cosmosItems = reclaimItems.Select(x => EfinItem.CreateEfinItem(CosmosDbConfiguration.PartitonKey, JObject.FromObject(x), idempotencyKey, "test", CosmosDbItemType.Reclaim));

        _efinCosmosClient.GetAllNewItemsAsync(CosmosDbItemType.Reclaim, "test").Returns(cosmosItems);

        var index = EfinIndexItem.Create(
            CosmosDbConfiguration.PartitonKey,
            CosmosDbItemType.Claim,
            IndexConfiguration.Reclaim.BatchIndex,
            IndexConfiguration.Reclaim.BatchIndexLength,
            IndexConfiguration.Reclaim.BatchIndexPrefix);

        _efinIndexCosmosClient.GetNextIndex(IndexConfiguration.Reclaim.BatchIndex, CosmosDbItemType.Reclaim).Returns(index);

        // Act
        await _function.Run(new TimerInfo(), CancellationToken.None);

        // Assert
        await _efinCosmosClient.Received(1).GetAllNewItemsAsync(CosmosDbItemType.Reclaim, "test");
        await _efinIndexCosmosClient.Received(1).GetNextIndex(IndexConfiguration.Reclaim.BatchIndex, CosmosDbItemType.Reclaim);
        await _fileWriter.Received(6).WriteAsync(Arg.Any<string>(), Arg.Any<BlobData>());
    }
}
