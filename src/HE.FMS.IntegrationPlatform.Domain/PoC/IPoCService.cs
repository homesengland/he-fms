using HE.FMS.IntegrationPlatform.Contract.PoC;

namespace HE.FMS.IntegrationPlatform.Domain.PoC;

public interface IPoCService
{
    Task PushToCosmosDb(string messageId, InputMessage message, CancellationToken cancellationToken);
}
