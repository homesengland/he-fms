namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Auth;

public interface IMambuApiKeyProvider
{
    Task<string> GetApiKey(CancellationToken cancellationToken);

    void InvalidateApiKey();
}
