namespace HE.FMS.IntegrationPlatform.BusinessLogic.Mambu;

public interface IMambuApiKeyService
{
    Task RotateApiKey(CancellationToken cancellationToken);

    Task HealthCheck(CancellationToken cancellationToken);
}
