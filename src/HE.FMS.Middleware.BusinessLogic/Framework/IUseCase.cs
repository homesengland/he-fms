namespace HE.FMS.Middleware.BusinessLogic.Framework;

public interface IUseCase<in TInput, TOutput>
{
    Task<TOutput> Trigger(TInput input, CancellationToken cancellationToken);
}
