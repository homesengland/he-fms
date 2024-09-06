using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;
using HE.FMS.Middleware.Providers.KeyVault;
using HE.FMS.Middleware.Providers.KeyVault.Settings;
using HE.FMS.Middleware.Providers.Mambu;
using HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement;
using HE.FMS.Middleware.Providers.Mambu.Api.Group;
using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount;
using HE.FMS.Middleware.Providers.Mambu.Api.Rotation;
using HE.FMS.Middleware.Providers.Mambu.Auth;
using HE.FMS.Middleware.Providers.Mambu.Extensions;
using HE.FMS.Middleware.Providers.Mambu.Settings;
using HE.FMS.Middleware.Providers.ServiceBus;
using Microsoft.Extensions.DependencyInjection;

namespace HE.FMS.Middleware.Providers.Config;

public static class ProvidersModule
{
    public static IServiceCollection AddProvidersModule(this IServiceCollection services)
    {
        return services.AddMambu()
            .AddCosmosDb()
            .AddKeyVault()
            .AddServiceBus();
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
        services.AddMambuApiClient<IMambuLoanAccountApiClient, MambuLoanAccountApiClient>().WithApiKeyAuthorization().WithDefaultRetryPolicy();

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

    private static IServiceCollection AddServiceBus(this IServiceCollection services)
    {
        services.AddSingleton<ITopicClientFactory, TopicClientFactory>();
        services.AddScoped<IConfigurationClient, ConfigurationClient>();

        return services;
    }
}
