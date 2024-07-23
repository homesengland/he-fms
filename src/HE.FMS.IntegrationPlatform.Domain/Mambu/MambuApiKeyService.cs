using System.Text.Json;
using HE.FMS.IntegrationPlatform.Providers.KeyVault;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Rotation;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Rotation.Contract;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Auth;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Settings;
using Microsoft.Extensions.Logging;

namespace HE.FMS.IntegrationPlatform.Domain.Mambu;

public class MambuApiKeyService : IMambuApiKeyService
{
    private readonly IMambuRotationApiClient _rotationApiClient;

    private readonly IKeyVaultSecretClient _keyVaultClient;

    private readonly IMambuApiKeyProvider _apiKeyProvider;

    private readonly IMambuApiKeySettings _settings;

    private readonly ILogger<MambuApiKeyService> _logger;

    public MambuApiKeyService(
        IMambuRotationApiClient rotationApiClient,
        IKeyVaultSecretClient keyVaultClient,
        IMambuApiKeyProvider apiKeyProvider,
        IMambuApiKeySettings settings,
        ILogger<MambuApiKeyService> logger)
    {
        _rotationApiClient = rotationApiClient;
        _keyVaultClient = keyVaultClient;
        _apiKeyProvider = apiKeyProvider;
        _settings = settings;
        _logger = logger;
    }

    public async Task RotateApiKey(CancellationToken cancellationToken)
    {
        var key = await _keyVaultClient.Get<RotateApiKeyResponse>(_settings.KeyVaultSecretName, cancellationToken);
        var response = await _rotationApiClient.RotateApiKey(key.Id, key.ApiKey, key.SecretKey, cancellationToken);

        try
        {
            await _keyVaultClient.Set(_settings.KeyVaultSecretName, response, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Mambu API rotated but failed to save the new key in KeyVault, update it manually, Key: {ApiKey}.",
                JsonSerializer.Serialize(key));

            throw;
        }

        _apiKeyProvider.InvalidateApiKey();
    }
}
