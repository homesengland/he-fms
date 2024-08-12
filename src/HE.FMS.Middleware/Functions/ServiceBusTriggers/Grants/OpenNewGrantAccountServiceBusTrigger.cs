using System.Text;
using HE.FMS.Middleware.BusinessLogic.Framework;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Grants.Results;
using HE.FMS.Middleware.Contract.Grants.UseCases;
using HE.FMS.Middleware.Providers.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.ServiceBus;

namespace HE.FMS.Middleware.Functions.ServiceBusTriggers.Grants;

public class OpenNewGrantAccountServiceBusTrigger
{
    private readonly IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult> _useCase;

    private readonly IStreamSerializer _streamSerializer;
    private readonly IObjectSerializer _objectSerializer;
    private readonly TopicClient _topicClient;

    public OpenNewGrantAccountServiceBusTrigger(
        IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult> useCase,
        IStreamSerializer streamSerializer,
        IObjectSerializer objectSerializer,
        ITopicClientFactory topicClientFactory)
    {
        _useCase = useCase;
        _streamSerializer = streamSerializer;
        _objectSerializer = objectSerializer;
        _topicClient = topicClientFactory.GetTopicClient("ServiceBus:PushToCrm:Topic");
    }

    [Function(nameof(OpenNewGrantAccountServiceBusTrigger))]
    [FixedDelayRetry(Constants.FunctionsConfiguration.MaxRetryCount, Constants.FunctionsConfiguration.DelayInterval)]
    public async Task Run(
        [ServiceBusTrigger("%Grants:OpenGrantAccount:TopicName%", "%Grants:OpenGrantAccount:SubscriptionName%", Connection = "ServiceBus:Connection")]
        Azure.Messaging.ServiceBus.ServiceBusReceivedMessage message,
        CancellationToken cancellationToken)
    {
        var dto = await _streamSerializer.Deserialize<OpenNewGrantAccountRequest>(message.Body.ToStream(), cancellationToken);

        var result = await _useCase.Trigger(dto, cancellationToken);

        var topicOutput = new Message(Encoding.UTF8.GetBytes(_objectSerializer.Serialize(result)))
        {
            CorrelationId = message.CorrelationId,
        };

        await _topicClient.SendAsync(topicOutput);
    }
}
