using System.Text.Json;
using HE.FMS.Middleware.BusinessLogic.Grants.Settings;
using HE.FMS.Middleware.Providers.KeyVault;
using HE.FMS.Middleware.Providers.Mambu.Api.Common;
using HE.FMS.Middleware.Providers.Mambu.Api.Group;
using HE.FMS.Middleware.Providers.Mambu.Api.Group.Contract;
using HE.FMS.Middleware.Providers.Mambu.Api.Rotation;
using HE.FMS.Middleware.Providers.Mambu.Api.Rotation.Contract;
using HE.FMS.Middleware.Providers.Mambu.Auth;
using HE.FMS.Middleware.Providers.Mambu.Settings;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.BusinessLogic.Mambu;

public class MambuApiKeyService : IMambuApiKeyService
{
    private readonly IMambuRotationApiClient _rotationApiClient;

    private readonly IMambuGroupApiClient _groupApiClient;

    private readonly IKeyVaultSecretClient _keyVaultClient;

    private readonly IMambuApiKeyProvider _apiKeyProvider;

    private readonly IMambuApiKeySettings _settings;

    private readonly IGrantsSettings _grantsSettings;

    private readonly ILogger<MambuApiKeyService> _logger;

    public MambuApiKeyService(
        IMambuRotationApiClient rotationApiClient,
        IMambuGroupApiClient groupApiClient,
        IKeyVaultSecretClient keyVaultClient,
        IMambuApiKeyProvider apiKeyProvider,
        IMambuApiKeySettings settings,
        IGrantsSettings grantsSettings,
        ILogger<MambuApiKeyService> logger)
    {
        _rotationApiClient = rotationApiClient;
        _groupApiClient = groupApiClient;
        _keyVaultClient = keyVaultClient;
        _apiKeyProvider = apiKeyProvider;
        _settings = settings;
        _grantsSettings = grantsSettings;
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

    public async Task HealthCheck(CancellationToken cancellationToken)
    {
        var response = await _groupApiClient.GetAll(
            new GetAllGroupsParams(BranchId: _grantsSettings.BranchId),
            new PageDetails(),
            cancellationToken);

        _logger.LogInformation("Mambu service is healthy. Groups count: {GroupsCount}", response.Count);
    }
}
