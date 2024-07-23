using HE.FMS.IntegrationPlatform.Common.Serialization;
using HE.FMS.IntegrationPlatform.Contract.PoC;
using HE.FMS.IntegrationPlatform.Domain.PoC;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HE.FMS.IntegrationPlatform.Functions.ServiceBusTriggers;

public class PoCPushMessageToCosmosDbServiceBusTrigger
{
    private readonly IPoCService _pocService;

    private readonly IBinarySerializer _binarySerializer;

    private readonly ILogger<PoCPushMessageToCosmosDbServiceBusTrigger> _logger;

    public PoCPushMessageToCosmosDbServiceBusTrigger(IPoCService pocService, IBinarySerializer binarySerializer, ILogger<PoCPushMessageToCosmosDbServiceBusTrigger> logger)
    {
        _pocService = pocService;
        _binarySerializer = binarySerializer;
        _logger = logger;
    }

    [Function(nameof(PoCPushMessageToCosmosDbServiceBusTrigger))]
    public async Task Run(
        [ServiceBusTrigger("%ServiceBus:PushMessageToCosmosDb:Topic%", "%ServiceBus:PushMessageToCosmosDb:Subscription%", Connection = "ServiceBus:Connection")]
        Azure.Messaging.ServiceBus.ServiceBusReceivedMessage message,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Message ID: {Id}", message.MessageId);
        _logger.LogInformation("Message Body: {Body}", message.Body);
        _logger.LogInformation("Message Content-Type: {ContentType}", message.ContentType);

        var inputMessage = _binarySerializer.Deserialize<InputMessage>(message.Body);

        await _pocService.PushToCosmosDb(message.MessageId, inputMessage, cancellationToken);
    }
}
