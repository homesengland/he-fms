using HE.FMS.IntegrationPlatform.BusinessLogic.PoC;
using HE.FMS.IntegrationPlatform.Common.Serialization;
using HE.FMS.IntegrationPlatform.Contract.PoC;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HE.FMS.IntegrationPlatform.Functions.ServiceBusTriggers;

public class PoCPushMessageToCosmosDbServiceBusTrigger
{
    private readonly IPoCService _pocService;

    private readonly IStreamSerializer _streamSerializer;

    private readonly ILogger<PoCPushMessageToCosmosDbServiceBusTrigger> _logger;

    public PoCPushMessageToCosmosDbServiceBusTrigger(IPoCService pocService, IStreamSerializer streamSerializer, ILogger<PoCPushMessageToCosmosDbServiceBusTrigger> logger)
    {
        _pocService = pocService;
        _streamSerializer = streamSerializer;
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

        var inputMessage = await _streamSerializer.Deserialize<InputMessage>(message.Body.ToStream(), cancellationToken);

        await _pocService.PushToCosmosDb(message.MessageId, inputMessage, cancellationToken);
    }
}
