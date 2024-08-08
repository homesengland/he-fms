using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Functions.ServiceBusTriggers;
public class PushToCrmServiceBusTrigger
{
    private readonly ILogger<PushToCrmServiceBusTrigger> _logger;

    public PushToCrmServiceBusTrigger(ILogger<PushToCrmServiceBusTrigger> logger)
    {
        _logger = logger;
    }

    [Function(nameof(PushToCrmServiceBusTrigger))]
    [CosmosDBOutput("%CosmosDb:DatabaseId%", "%CosmosDb:ContainerId%", Connection = "CosmosDb:ConnectionString", PartitionKey = "PoC")]
    public async Task<ServiceBusReceivedMessage> Run(
        [ServiceBusTrigger("%ServiceBus:PushToCrm:Topic%", "%ServiceBus:PushToCrm:Subscription%", Connection = "ServiceBus:Connection")]
        ServiceBusReceivedMessage message,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Message ID: {Id}", message.MessageId);
        _logger.LogInformation("Message Body: {Body}", message.Body);
        _logger.LogInformation("Message Content-Type: {ContentType}", message.ContentType);

        // TODO: send to CRM
        return await Task.FromResult(message);
    }
}
