using HE.FMS.IntegrationPlatform.Common.Extensions;
using HE.FMS.IntegrationPlatform.Providers.CosmosDb;
using HE.FMS.IntegrationPlatform.Providers.CosmosDb.Settings;
using HE.FMS.IntegrationPlatform.Providers.KeyVault;
using HE.FMS.IntegrationPlatform.Providers.KeyVault.Settings;
using HE.FMS.IntegrationPlatform.Providers.Mambu;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Rotation;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Auth;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Extensions;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace HE.FMS.IntegrationPlatform.Providers.Config;

public static class ProvidersModule
{
    public static IServiceCollection AddProvidersModule(this IServiceCollection services)
    {
        return services.AddMambu()
            .AddCosmosDb()
            .AddKeyVault();
    }

    private static IServiceCollection AddMambu(this IServiceCollection services)
    {
        services.AddAppConfiguration<IMambuApiSettings, MambuApiSettings>("Mambu:Api");
        services.AddAppConfiguration<IMambuApiKeySettings, MambuApiKeySettings>("Mambu:ApiKey");
        services.AddScoped<MambuApiKeyAuthorizationHandler>();
        services.AddScoped<IMambuApiKeyProvider, MambuApiKeyProvider>();
        services.Decorate<IMambuApiKeyProvider, MambuCachedApiKeyProviderDecorator>();

        services.AddMambuApiClient<IMambuRotationApiClient, MambuRotationApiClient>().WithDefaultRetryPolicy();
        services.AddMambuApiClient<IMambuGroupApiClient, MambuGroupApiClient>().WithApiKeyAuthorization().WithDefaultRetryPolicy();
        services.AddMambuApiClient<IMambuCreditArrangementApiClient, MambuCreditArrangementApiClient>().WithApiKeyAuthorization().WithDefaultRetryPolicy();

        return services;
    }

    private static IServiceCollection AddCosmosDb(this IServiceCollection services)
    {
        services.AddAppConfiguration<ICosmosDbSettings, CosmosDbSettings>("CosmosDb");
        services.AddScoped<ICosmosDbClient, CosmosDbClient>();

        return services;
    }

    private static IServiceCollection AddKeyVault(this IServiceCollection services)
    {
        services.AddAppConfiguration<IKeyVaultSettings, KeyVaultSettings>("KeyVault");
        services.AddScoped<IKeyVaultSecretClient, KeyVaultSecretClient>();

        return services;
    }

    private static IHttpClientBuilder AddMambuApiClient<TService, TImplementation>(this IServiceCollection services)
        where TImplementation : MambuApiHttpClientBase, TService
        where TService : class
    {
        return services.AddHttpClient<TService, TImplementation>(
            (serviceProvider, httpClient) =>
            {
                var settings = serviceProvider.GetRequiredService<IMambuApiSettings>();
                httpClient.BaseAddress = settings.BaseUrl;
            });
    }
}
