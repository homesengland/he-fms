using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using HE.FMS.Middleware.BusinessLogic.Framework;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Grants.Results;
using HE.FMS.Middleware.Contract.Grants.UseCases;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Azure;

namespace HE.FMS.Middleware.Functions.ServiceBusTriggers.Grants;

public class OpenNewGrantAccountServiceBusTrigger
{
    private readonly IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult> _useCase;

    private readonly IStreamSerializer _streamSerializer;
    private readonly IObjectSerializer _objectSerializer;
    private readonly ServiceBusSender _serviceBusSender;

    public OpenNewGrantAccountServiceBusTrigger(
        IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult> useCase,
        IStreamSerializer streamSerializer,
        IObjectSerializer objectSerializer,
        IAzureClientFactory<ServiceBusClient> clientFactory)
    {
        _useCase = useCase;
        _streamSerializer = streamSerializer;
        _objectSerializer = objectSerializer;
        _serviceBusSender = _serviceBusSender = clientFactory
            .CreateClient(Constants.Settings.ServiceBus.DefaultClientName)
            .CreateSender("ServiceBus:PushToCrm:Topic");
    }

    [Function(nameof(OpenNewGrantAccountServiceBusTrigger))]
    [FixedDelayRetry(Constants.FunctionsConfiguration.MaxRetryCount, Constants.FunctionsConfiguration.DelayInterval)]
    public async Task Run(
        [ServiceBusTrigger("%Grants:OpenGrantAccount:TopicName%", "%Grants:OpenGrantAccount:SubscriptionName%", Connection = "ServiceBus:Connection")]
        ServiceBusReceivedMessage message,
        CancellationToken cancellationToken)
    {
        var inputData = await _streamSerializer.Deserialize<OpenNewGrantAccountRequest>(message.Body.ToStream(), cancellationToken);

        await _useCase.Trigger(inputData, cancellationToken);

        ServiceBusMessage sbMessage = new(Encoding.UTF8.GetBytes(_objectSerializer.Serialize(inputData)))
        {
            CorrelationId = message.CorrelationId,
        };

        await _serviceBusSender.SendMessageAsync(sbMessage, cancellationToken);
    }
}
