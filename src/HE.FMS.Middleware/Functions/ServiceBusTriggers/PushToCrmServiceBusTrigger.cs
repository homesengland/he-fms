using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Grants.Results;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Functions.ServiceBusTriggers;
public class PushToCrmServiceBusTrigger
{
    private readonly ILogger<PushToCrmServiceBusTrigger> _logger;

    private readonly IStreamSerializer _streamSerializer;

    public PushToCrmServiceBusTrigger(
        IStreamSerializer streamSerializer,
        ILogger<PushToCrmServiceBusTrigger> logger)
    {
        _streamSerializer = streamSerializer;
        _logger = logger;
    }

    [Function(nameof(PushToCrmServiceBusTrigger))]
    [CosmosDBOutput("%CosmosDb:DatabaseId%", "%CosmosDb:ContainerId%", Connection = "CosmosDb:ConnectionString")]
    public async Task<TraceItem> Run(
        [ServiceBusTrigger("%ServiceBus:PushToCrm:Topic%", "%ServiceBus:PushToCrm:Subscription%", Connection = "ServiceBus:Connection")]
        ServiceBusReceivedMessage message,
        CancellationToken cancellationToken)
    {
        var inputData = await _streamSerializer.Deserialize<OpenNewGrantAccountResult>(message.Body.ToStream(), cancellationToken);

        var cosmosDbOutput = TraceItem.CreateTraceItem(Constants.CosmosDbConfiguration.PartitonKey, inputData, message.CorrelationId, string.Empty, CosmosDbItemType.Log);

        _logger.LogInformation("Message Id: {Id}", message.MessageId);
        _logger.LogInformation("Message Correlation Id: {CorrelationId}", message.CorrelationId);
        _logger.LogInformation("Message Body: {Body}", message.Body);
        _logger.LogInformation("Message Content-Type: {ContentType}", message.ContentType);

        // TODO: send to CRM
        return await Task.FromResult(cosmosDbOutput);
    }
}
