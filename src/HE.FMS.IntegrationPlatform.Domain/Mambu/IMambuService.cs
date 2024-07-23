namespace HE.FMS.IntegrationPlatform.Domain.Mambu;

public interface IMambuService
{
    Task HealthCheck(CancellationToken cancellationToken);
}
