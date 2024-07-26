namespace HE.FMS.IntegrationPlatform.Domain.Mambu;

public interface IMambuApiKeyService
{
    Task RotateApiKey(CancellationToken cancellationToken);

    Task HealthCheck(CancellationToken cancellationToken);
}
