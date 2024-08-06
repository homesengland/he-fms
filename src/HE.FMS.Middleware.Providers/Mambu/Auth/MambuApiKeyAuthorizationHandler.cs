namespace HE.FMS.Middleware.Providers.Mambu.Auth;

internal sealed class MambuApiKeyAuthorizationHandler : DelegatingHandler
{
    private readonly IMambuApiKeyProvider _apiKeyProvider;

    public MambuApiKeyAuthorizationHandler(IMambuApiKeyProvider apiKeyProvider)
    {
        _apiKeyProvider = apiKeyProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var apiKey = await _apiKeyProvider.GetApiKey(cancellationToken);
        request.Headers.Add("apiKey", apiKey);

        return await base.SendAsync(request, cancellationToken);
    }
}
