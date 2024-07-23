using HE.FMS.IntegrationPlatform.Providers.KeyVault;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Rotation.Contract;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Settings;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Auth;

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
        var keys = await _keyVaultClient.Get<RotateApiKeyResponse>(_settings.KeyVaultSecretName, cancellationToken);
        return keys.ApiKey;
    }

    public void InvalidateApiKey()
    {
    }
}
