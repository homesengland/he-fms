using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Functions.ServiceBusTriggers;
public class PushToCrmServiceBusTrigger(ILogger<PushToCrmServiceBusTrigger> logger)
{
    [Function(nameof(PushToCrmServiceBusTrigger))]
    [CosmosDBOutput("%CosmosDb:DatabaseId%", "%CosmosDb:ContainerId%", Connection = "CosmosDb:ConnectionString", PartitionKey = "PoC")]
    public async Task<ServiceBusReceivedMessage> Run(
        [ServiceBusTrigger("%ServiceBus:PushToCrm:Topic%", "%ServiceBus:PushToCrm:Subscription%", Connection = "ServiceBus:Connection")]
        ServiceBusReceivedMessage message,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Message ID: {Id}", message.MessageId);
        logger.LogInformation("Message Body: {Body}", message.Body);
        logger.LogInformation("Message Content-Type: {ContentType}", message.ContentType);

        // TODO: send to CRM

        return message;
    }
}
