using HE.FMS.Middleware.Contract.Mambu.Rotation;

namespace HE.FMS.Middleware.Providers.Mambu.Api.Rotation;

public interface IMambuRotationApiClient
{
    Task<RotateApiKeyResponse> RotateApiKey(string keyId, string apiKey, string secretKey, CancellationToken cancellationToken);
}
