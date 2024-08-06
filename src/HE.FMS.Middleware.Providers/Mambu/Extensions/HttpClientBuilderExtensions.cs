using System.Net;
using HE.FMS.Middleware.Providers.Mambu.Auth;
using HE.FMS.Middleware.Providers.Mambu.Settings;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;

namespace HE.FMS.Middleware.Providers.Mambu.Extensions;

internal static class HttpClientBuilderExtensions
{
    public static IHttpClientBuilder WithApiKeyAuthorization(this IHttpClientBuilder httpClientBuilder)
    {
        return httpClientBuilder.AddHttpMessageHandler<MambuApiKeyAuthorizationHandler>()
            .AddPolicyHandler((provider, _) => ConfigureRetryPolicy(provider));
    }

    public static IHttpClientBuilder WithDefaultRetryPolicy(this IHttpClientBuilder httpClientBuilder)
    {
        return httpClientBuilder.AddPolicyHandler((provider, _) => ConfigureRetryPolicy(provider));
    }

    private static AsyncRetryPolicy<HttpResponseMessage> ConfigureRetryPolicy(IServiceProvider serviceProvider)
    {
        var settings = serviceProvider.GetRequiredService<IMambuApiSettings>();

        return Policy.HandleResult<HttpResponseMessage>(
                x => x.StatusCode is HttpStatusCode.RequestTimeout || x.StatusCode >= HttpStatusCode.InternalServerError)
            .WaitAndRetryAsync(settings.RetryCount, _ => TimeSpan.FromMilliseconds(settings.RetryDelayInMilliseconds));
    }
}
