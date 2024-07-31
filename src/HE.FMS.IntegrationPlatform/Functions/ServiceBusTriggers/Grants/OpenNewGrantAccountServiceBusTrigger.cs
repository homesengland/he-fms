using HE.FMS.IntegrationPlatform.BusinessLogic.Framework;
using HE.FMS.IntegrationPlatform.Common.Serialization;
using HE.FMS.IntegrationPlatform.Contract.Grants.Results;
using HE.FMS.IntegrationPlatform.Contract.Grants.UseCases;
using Microsoft.Azure.Functions.Worker;

namespace HE.FMS.IntegrationPlatform.Functions.ServiceBusTriggers.Grants;

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
    public async Task Run(
        [ServiceBusTrigger("%Grants:OpenGrantAccount:TopicName%", "%Grants:OpenGrantAccount:SubscriptionName%", Connection = "ServiceBus:Connection")]
        Azure.Messaging.ServiceBus.ServiceBusReceivedMessage message,
        CancellationToken cancellationToken)
    {
        var dto = await _streamSerializer.Deserialize<OpenNewGrantAccountRequest>(message.Body.ToStream(), cancellationToken);

        await _useCase.Trigger(dto, cancellationToken);
    }
}
