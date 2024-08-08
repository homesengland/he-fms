using HE.FMS.Middleware.BusinessLogic.Framework;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Grants.Results;
using HE.FMS.Middleware.Contract.Grants.UseCases;
using Microsoft.Azure.Functions.Worker;

namespace HE.FMS.Middleware.Functions.ServiceBusTriggers.Grants;

public class OpenNewGrantAccountServiceBusTrigger
{
    private readonly IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult> _useCase;

    private readonly IStreamSerializer _streamSerializer;

    public OpenNewGrantAccountServiceBusTrigger(
        IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult> useCase,
        IStreamSerializer streamSerializer)
    {
        _useCase = useCase;
        _streamSerializer = streamSerializer;
    }

    [Function(nameof(OpenNewGrantAccountServiceBusTrigger))]
    [ServiceBusOutput("%ServiceBus:PushToCrm:Topic%", ServiceBusEntityType.Topic, Connection = "ServiceBus:Connection")]
    [FixedDelayRetry(Constants.FunctionsConfiguration.MaxRetryCount, Constants.FunctionsConfiguration.DelayInterval)]
    public async Task<OpenNewGrantAccountResult> Run(
        [ServiceBusTrigger("%Grants:OpenGrantAccount:TopicName%", "%Grants:OpenGrantAccount:SubscriptionName%", Connection = "ServiceBus:Connection")]
        Azure.Messaging.ServiceBus.ServiceBusReceivedMessage message,
        CancellationToken cancellationToken)
    {
        var dto = await _streamSerializer.Deserialize<OpenNewGrantAccountRequest>(message.Body.ToStream(), cancellationToken);

        return await _useCase.Trigger(dto, cancellationToken);
    }
}
