using System.Net;
using HE.FMS.IntegrationPlatform.Common.Extensions;
using HE.FMS.IntegrationPlatform.Providers.CosmosDb;
using HE.FMS.IntegrationPlatform.Providers.CosmosDb.Settings;
using HE.FMS.IntegrationPlatform.Providers.Mambu;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Auth;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Settings;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;

namespace HE.FMS.IntegrationPlatform.Providers.Config;

public static class ProvidersModule
{
    public static IServiceCollection AddProvidersModule(this IServiceCollection services)
    {
        return services.AddMambu()
            .AddCosmosDb();
    }

    private static IServiceCollection AddMambu(this IServiceCollection services)
    {
        services.AddAppConfiguration<IMambuSettings, MambuSettings>("Mambu");
        services.AddHttpClient<IMambuApiHttpClient, MambuApiHttpClient>(
                (serviceProvider, httpClient) =>
                {
                    var settings = serviceProvider.GetRequiredService<IMambuSettings>();
                    httpClient.BaseAddress = settings.BaseUrl;
                })
            .AddHttpMessageHandler<MambuAuthorizationHandler>()
            .AddPolicyHandler((provider, _) => ConfigureRetryPolicy(provider));

        return services;
    }

    private static IServiceCollection AddCosmosDb(this IServiceCollection services)
    {
        services.AddAppConfiguration<ICosmosDbSettings, CosmosDbSettings>("CosmosDb");
        services.AddScoped<ICosmosDbClient, CosmosDbClient>();

        return services;
    }

    private static AsyncRetryPolicy<HttpResponseMessage> ConfigureRetryPolicy(IServiceProvider serviceProvider)
    {
        var settings = serviceProvider.GetRequiredService<IMambuSettings>();

        return Policy.HandleResult<HttpResponseMessage>(
                x => x.StatusCode is HttpStatusCode.RequestTimeout || x.StatusCode >= HttpStatusCode.InternalServerError)
            .WaitAndRetryAsync(settings.RetryCount, _ => TimeSpan.FromMilliseconds(settings.RetryDelayInMilliseconds));
    }
}
