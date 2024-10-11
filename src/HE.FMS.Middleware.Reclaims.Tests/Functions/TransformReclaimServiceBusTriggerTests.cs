using Azure.Messaging.ServiceBus;
using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
using HE.FMS.Middleware.BusinessLogic.Tests.Factories;
using HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Config;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Common.Tests.Helpers;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Providers.Common;
using HE.FMS.Middleware.Providers.Common.Settings;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;
using HE.FMS.Middleware.Reclaims.Functions;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.Reclaims.Tests.Functions;
public class TransformReclaimServiceBusTriggerTests : IDisposable
{
    private readonly Container _cosmosContainer;
    private readonly EfinCosmosClient _efinCosmosClient;

    private readonly TransformReclaimServiceBusTrigger _function;

    private bool _disposed;

    public TransformReclaimServiceBusTriggerTests()
    {
        var cosmosDbSettings = new CosmosDbSettings { DatabaseId = "test", ContainerId = "test" };
        var allowedEnvironmentSettings = new AllowedEnvironmentSettings("test");

        var logger = Substitute.For<ILogger<TransformReclaimServiceBusTrigger>>();
        var streamSerializer = new StreamSerializer(CommonModule.CommonSerializerOptions, Substitute.For<ILogger<StreamSerializer>>());
        var environmentValidator = new EnvironmentValidator(allowedEnvironmentSettings);

        var cosmosClient = Substitute.For<CosmosClient>();
        var cosmosDatabase = Substitute.For<Database>();
        _cosmosContainer = Substitute.For<Container>();
        cosmosClient.GetDatabase(cosmosDbSettings.DatabaseId).Returns(cosmosDatabase);
        cosmosDatabase.GetContainer(cosmosDbSettings.ContainerId).Returns(_cosmosContainer);

        var reclaimConverter = new ReclaimConverter(new FakeDateTimeProvider(), new FakeEfinLookupService());

        _efinCosmosClient = new EfinCosmosClient(cosmosClient, cosmosDbSettings);

        _function = new TransformReclaimServiceBusTrigger(
            streamSerializer,
            reclaimConverter,
            _efinCosmosClient,
            environmentValidator,
            logger);
    }

    ~TransformReclaimServiceBusTriggerTests()
    {
        Dispose(false);
    }

    [Fact]
    public async Task Run_ShouldTransformReclaim()
    {
        // Arrange
        var idempotencyKey = Guid.NewGuid().ToString();
        var payload = PaymentRequestFactory.CreateRandomReclaimPaymentRequest();
        var binaryData = await BinaryData.FromStreamAsync(TestingHelper.SerializeToStream(payload));
        var properties = new Dictionary<string, object> { { Constants.CustomHeaders.Environment, "test" }, };

        var serviceBusReceivedMessage = ServiceBusModelFactory.ServiceBusReceivedMessage(body: binaryData, correlationId: idempotencyKey, properties: properties);

        // Act
        await _function.Run(serviceBusReceivedMessage, CancellationToken.None);

        // Assert
        await _cosmosContainer.Received(1).UpsertItemAsync(Arg.Any<EfinItem>(), Arg.Any<PartitionKey>(), Arg.Any<ItemRequestOptions>(), Arg.Any<CancellationToken>());
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _efinCosmosClient.Dispose();
            }

            _disposed = true;
        }
    }
}