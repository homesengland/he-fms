namespace HE.FMS.IntegrationPlatform.Providers.Mambu;

internal interface IMambuApiHttpClient
{
    Task<TResponse> Send<TRequest, TResponse>(HttpMethod httpMethod, string relativeUrl, TRequest requestBody, CancellationToken cancellationToken);
}
