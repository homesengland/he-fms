using Azure.Messaging.ServiceBus;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Grants.Results;
using HE.FMS.Middleware.Providers.CosmosDb;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Functions.ServiceBusTriggers;
public class PushToCrmServiceBusTrigger
{
    private readonly ILogger<PushToCrmServiceBusTrigger> _logger;

    private readonly IStreamSerializer _streamSerializer;
    private readonly CosmosDbHelper _cosmosDbHelper;

    public PushToCrmServiceBusTrigger(
        IStreamSerializer streamSerializer,
        CosmosDbHelper cosmosDbHelper,
        ILogger<PushToCrmServiceBusTrigger> logger)
    {
        _streamSerializer = streamSerializer;
        _cosmosDbHelper = cosmosDbHelper;
        _logger = logger;
    }

    [Function(nameof(PushToCrmServiceBusTrigger))]
    [CosmosDBOutput("%CosmosDb:DatabaseId%", "%CosmosDb:ContainerId%", Connection = "CosmosDb:ConnectionString")]
    public async Task<CosmosDbItem> Run(
        [ServiceBusTrigger("%ServiceBus:PushToCrm:Topic%", "%ServiceBus:PushToCrm:Subscription%", Connection = "ServiceBus:Connection")]
        ServiceBusReceivedMessage message,
        CancellationToken cancellationToken)
    {
        var inputData = await _streamSerializer.Deserialize<OpenNewGrantAccountResult>(message.Body.ToStream(), cancellationToken);

        var cosmosDbOutput = _cosmosDbHelper.CreateCosmosDbItem(inputData, message.CorrelationId);

        _logger.LogInformation("Message Id: {Id}", message.MessageId);
        _logger.LogInformation("Message Correlation Id: {CorrelationId}", message.CorrelationId);
        _logger.LogInformation("Message Body: {Body}", message.Body);
        _logger.LogInformation("Message Content-Type: {ContentType}", message.ContentType);

        // TODO: send to CRM
        return await Task.FromResult(cosmosDbOutput);
    }
}
