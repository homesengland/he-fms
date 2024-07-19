using HE.FMS.IntegrationPlatform.Common.Extensions;
using HE.FMS.IntegrationPlatform.Providers.CosmosDb;
using HE.FMS.IntegrationPlatform.Providers.CosmosDb.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace HE.FMS.IntegrationPlatform.Providers.Config;

public static class ProvidersModule
{
    public static IServiceCollection AddProvidersModule(this IServiceCollection services)
    {
        return services.AddCosmosDb();
    }

    private static IServiceCollection AddCosmosDb(this IServiceCollection services)
    {
        services.AddAppConfiguration<ICosmosDbSettings, CosmosDbSettings>("CosmosDb");
        services.AddScoped<ICosmosDbClient, CosmosDbClient>();

        return services;
    }
}
