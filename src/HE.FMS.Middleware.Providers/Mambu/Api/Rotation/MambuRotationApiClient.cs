using System.Diagnostics.CodeAnalysis;
using HE.FMS.Middleware.Contract.Mambu.Rotation;
using HE.FMS.Middleware.Providers.Mambu.Settings;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Providers.Mambu.Api.Rotation;

[ExcludeFromCodeCoverage]
internal sealed class MambuRotationApiClient : MambuApiHttpClientBase, IMambuRotationApiClient
{
    private readonly IMambuApiKeySettings _settings;

    public MambuRotationApiClient(HttpClient httpClient, IMambuApiKeySettings settings, ILogger<MambuRotationApiClient> logger)
        : base(httpClient, logger)
    {
        _settings = settings;
    }

    protected override string ApiName => "Mambu.RotationApi";

    public async Task<RotateApiKeyResponse> RotateApiKey(string keyId, string apiKey, string secretKey, CancellationToken cancellationToken)
    {
        return await Send<RotateApiKeyRequest, RotateApiKeyResponse>(
            HttpMethod.Post,
            "/api/apikey/rotation",
            new RotateApiKeyRequest(apiKey, _settings.ExpirationInSeconds, keyId),
            cancellationToken,
            httpRequestMessage => httpRequestMessage.Headers.Add("secretkey", secretKey));
    }
}
