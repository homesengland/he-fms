namespace HE.FMS.IntegrationPlatform.Domain.Framework;

public interface IUseCase<in TInput, TOutput>
{
    Task<TOutput> Trigger(TInput input, CancellationToken cancellationToken);
}
