using Azure.Messaging.ServiceBus;
using HE.FMS.Middleware.BusinessLogic.Tests.Factories;
using HE.FMS.Middleware.BusinessLogic.Trace.CosmosDb;
using HE.FMS.Middleware.Claims.Functions;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Config;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Common.Tests.Fakes;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Providers.Common;
using HE.FMS.Middleware.Providers.Common.Settings;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;
using HE.FMS.Middleware.Providers.SeriveBus.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using ObjectSerializer = HE.FMS.Middleware.Common.Serialization.ObjectSerializer;

namespace HE.FMS.Middleware.Claims.Tests.Functions;
public class ValidateClaimHttpTriggerTests : IAsyncDisposable
{
    private readonly IAzureClientFactory<ServiceBusClient> _clientFactory;
    private readonly Container _cosmosContainer;
#pragma warning disable CA2213 // Implemented async disposable pattern
    private readonly TraceCosmosClient _traceCosmosClient;
    private readonly ServiceBusSender _serviceBusSender;
#pragma warning restore CA2213

    private readonly ValidateClaimHttpTrigger _function;

    private bool _disposed;

    public ValidateClaimHttpTriggerTests()
    {
        var cosmosDbSettings = new CosmosDbSettings { DatabaseId = "test", ContainerId = "test" };
        var serviceBusSettings = new ServiceBusSettings { ClaimsTopic = "claims-topic" };
        var allowedEnvironmentSettings = new AllowedEnvironmentSettings("test");

        var logger = Substitute.For<ILogger<ValidateClaimHttpTrigger>>();
        var streamSerializer = new StreamSerializer(CommonModule.CommonSerializerOptions, Substitute.For<ILogger<StreamSerializer>>());
        var objectSerializer = new ObjectSerializer(CommonModule.CommonSerializerOptions);
        var environmentValidator = new EnvironmentValidator(allowedEnvironmentSettings);

        var cosmosClient = Substitute.For<CosmosClient>();
        var cosmosDatabase = Substitute.For<Database>();
        _cosmosContainer = Substitute.For<Container>();
        cosmosClient.GetDatabase(cosmosDbSettings.DatabaseId).Returns(cosmosDatabase);
        cosmosDatabase.GetContainer(cosmosDbSettings.ContainerId).Returns(_cosmosContainer);

        _traceCosmosClient = new TraceCosmosClient(cosmosClient, cosmosDbSettings);

        var serviceBusClient = Substitute.For<ServiceBusClient>();
        _serviceBusSender = Substitute.For<ServiceBusSender>();
        serviceBusClient.CreateSender(serviceBusSettings.ClaimsTopic).Returns(_serviceBusSender);
        _clientFactory = Substitute.For<IAzureClientFactory<ServiceBusClient>>();
        _clientFactory.CreateClient(Arg.Any<string>()).Returns(serviceBusClient);

        _function = new ValidateClaimHttpTrigger(
            streamSerializer,
            _traceCosmosClient,
            objectSerializer,
            _clientFactory,
            environmentValidator,
            serviceBusSettings,
            logger);
    }

    ~ValidateClaimHttpTriggerTests()
    {
        DisposeAsync(false).AsTask().GetAwaiter().GetResult();
    }

    [Fact]
    public async Task Run_ShouldValidateClaim()
    {
        // Arrange
        var idempotencyKey = Guid.NewGuid().ToString();
        var payload = PaymentRequestFactory.CreateRandomClaimPaymentRequest();

        var context = Substitute.For<FunctionContext>();
        var requestData = new FakeHttpRequestData(payload, context);
        requestData.Headers.Add(Constants.CustomHeaders.IdempotencyKey, [idempotencyKey]);
        requestData.Headers.Add(Constants.CustomHeaders.Environment, ["test"]);

        // Act
        await _function.Run(requestData, CancellationToken.None);

        // Assert
        await _cosmosContainer.Received(1).UpsertItemAsync(Arg.Any<TraceItem>(), Arg.Any<PartitionKey>(), Arg.Any<ItemRequestOptions>(), Arg.Any<CancellationToken>());
        await _serviceBusSender.Received(1).SendMessageAsync(Arg.Any<ServiceBusMessage>(), Arg.Any<CancellationToken>());
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                await _serviceBusSender.DisposeAsync();
                _traceCosmosClient.Dispose();
            }

            _disposed = true;
        }
    }
}
