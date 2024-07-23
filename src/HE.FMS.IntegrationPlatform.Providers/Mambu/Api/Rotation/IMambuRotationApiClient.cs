using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Rotation.Contract;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Rotation;

public interface IMambuRotationApiClient
{
    Task<RotateApiKeyResponse> RotateApiKey(string keyId, string apiKey, string secretKey, CancellationToken cancellationToken);
}
