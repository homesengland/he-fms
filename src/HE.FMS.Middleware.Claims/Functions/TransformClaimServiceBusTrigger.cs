using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Efin;
using HE.FMS.Middleware.Providers.Efin;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Claims.Functions;
public class TransformClaimServiceBusTrigger
{
    private readonly IStreamSerializer _streamSerializer;
    private readonly IClaimConverter _claimConverter;
    private readonly IEfinCosmosClient _efinCosmosDbClient;
    private readonly ILogger<TransformClaimServiceBusTrigger> _logger;

    public TransformClaimServiceBusTrigger(
        IStreamSerializer streamSerializer,
        IClaimConverter claimConverter,
        IEfinCosmosClient efinCosmosDbClient,
        ILogger<TransformClaimServiceBusTrigger> logger)
    {
        _streamSerializer = streamSerializer;
        _claimConverter = claimConverter;
        _efinCosmosDbClient = efinCosmosDbClient;
        _logger = logger;
    }

    [Function(nameof(TransformClaimServiceBusTrigger))]
    public async Task Run(
        [ServiceBusTrigger("%Claims:Create:TopicName%", "%Claims:Create:SubscriptionName%", Connection = "ServiceBus:Connection")]
        ServiceBusReceivedMessage message,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(TransformClaimServiceBusTrigger)} function started");

        var inputData = await _streamSerializer.Deserialize<ClaimPaymentRequest>(message.Body.ToStream(), cancellationToken);
        var convertedData = await _claimConverter.Convert(inputData);
        var cosmosDbOutput = EfinItem.CreateEfinItem(convertedData, message.CorrelationId, CosmosDbItemType.Claim);
        await _efinCosmosDbClient.UpsertItem(cosmosDbOutput, cancellationToken);

        _logger.LogInformation($"{nameof(TransformClaimServiceBusTrigger)} function ended");
    }
}
