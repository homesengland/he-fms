using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Contract.Reclaims;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Reclaims.Functions;
public class TransformReclaimServiceBusTrigger
{
    private readonly IStreamSerializer _streamSerializer;
    private readonly IReclaimConverter _reclaimConverter;
    private readonly IEfinCosmosClient _efinCosmosDbClient;
    private readonly ILogger<TransformReclaimServiceBusTrigger> _logger;

    public TransformReclaimServiceBusTrigger(
        IStreamSerializer streamSerializer,
        IReclaimConverter reclaimConverter,
        IEfinCosmosClient efinCosmosDbClient,
        ILogger<TransformReclaimServiceBusTrigger> logger)
    {
        _streamSerializer = streamSerializer;
        _reclaimConverter = reclaimConverter;
        _efinCosmosDbClient = efinCosmosDbClient;
        _logger = logger;
    }

    [Function(nameof(TransformReclaimServiceBusTrigger))]
    public async Task Run(
        [ServiceBusTrigger("%Reclaims:Create:TopicName%", "%Reclaims:Create:SubscriptionName%", Connection = "ServiceBus:Connection")]
        ServiceBusReceivedMessage message,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(TransformReclaimServiceBusTrigger)} function started");

        var inputData = await _streamSerializer.Deserialize<ReclaimPaymentRequest>(message.Body.ToStream(), cancellationToken);
        var convertedData = _reclaimConverter.CreateItems(inputData);
        var cosmosDbOutput = EfinItem.CreateEfinItem(Constants.CosmosDbConfiguration.PartitonKey, convertedData, message.CorrelationId, CosmosDbItemType.Reclaim);
        await _efinCosmosDbClient.UpsertItem(cosmosDbOutput, cancellationToken);

        _logger.LogInformation($"{nameof(TransformReclaimServiceBusTrigger)} function ended");
    }
}
