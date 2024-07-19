namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Auth;

internal sealed class MambuAuthorizationHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // TODO: AB#102721 Generic functionality for API Key management
        return base.SendAsync(request, cancellationToken);
    }
}
