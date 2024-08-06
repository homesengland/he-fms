namespace HE.FMS.Middleware.Providers.Mambu.Auth;

public interface IMambuApiKeyProvider
{
    Task<string> GetApiKey(CancellationToken cancellationToken);

    void InvalidateApiKey();
}
