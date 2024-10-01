using System.Diagnostics.CodeAnalysis;
using HE.FMS.Middleware.Contract.Mambu.Rotation;
using HE.FMS.Middleware.Providers.KeyVault;
using HE.FMS.Middleware.Providers.Mambu.Settings;

namespace HE.FMS.Middleware.Providers.Mambu.Auth;

[ExcludeFromCodeCoverage]
internal sealed class MambuApiKeyProvider : IMambuApiKeyProvider
{
    private readonly IKeyVaultSecretClient _keyVaultClient;

    private readonly IMambuApiKeySettings _settings;

    public MambuApiKeyProvider(IKeyVaultSecretClient keyVaultClient, IMambuApiKeySettings settings)
    {
        _keyVaultClient = keyVaultClient;
        _settings = settings;
    }

    public async Task<string> GetApiKey(CancellationToken cancellationToken)
    {
        var keys = await _keyVaultClient.Get<RotateApiKeyResponse>(_settings.KeyVaultValueName, cancellationToken);
        return keys.ApiKey;
    }

    public void InvalidateApiKey()
    {
    }
}
