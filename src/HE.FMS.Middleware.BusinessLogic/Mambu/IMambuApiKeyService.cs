namespace HE.FMS.Middleware.BusinessLogic.Mambu;

public interface IMambuApiKeyService
{
    Task RotateApiKey(CancellationToken cancellationToken);

    Task HealthCheck(CancellationToken cancellationToken);
}
