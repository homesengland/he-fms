using HE.FMS.Middleware.Contract.PoC;

namespace HE.FMS.Middleware.BusinessLogic.PoC;

public interface IPoCService
{
    Task PushToCosmosDb(string messageId, InputMessage message, CancellationToken cancellationToken);
}
