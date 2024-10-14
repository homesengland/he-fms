using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
using HE.FMS.Middleware.BusinessLogic.Trace.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HE.FMS.Middleware.BusinessLogic.Config;

public static class DomainModule
{
    public static IServiceCollection AddDomainModule(this IServiceCollection services)
    {
        return services
            .AddClaimReclaim();
    }

    internal static IServiceCollection AddClaimReclaim(this IServiceCollection services)
    {
        services.AddSingleton<IEfinCosmosClient, EfinCosmosClient>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var client = sp.GetRequiredService<CosmosClient>();
            var settings = sp.GetRequiredService<CosmosDbSettings>();
            settings.ContainerId = config["EfinDb:ContainerId"] ?? string.Empty;

            return new EfinCosmosClient(client, settings);
        });

        services.AddSingleton<IEfinIndexCosmosClient, EfinIndexCosmosClient>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var client = sp.GetRequiredService<CosmosClient>();
            var settings = sp.GetRequiredService<CosmosDbSettings>();
            settings.ContainerId = config["EfinConfigDb:ContainerId"] ?? string.Empty;

            return new EfinIndexCosmosClient(client, settings);
        });

        services.AddSingleton<IEfinLookupCosmosClient, EfinLookupCosmosClient>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var client = sp.GetRequiredService<CosmosClient>();
            var settings = sp.GetRequiredService<CosmosDbSettings>();
            settings.ContainerId = config["EfinConfigDb:ContainerId"] ?? string.Empty;

            return new EfinLookupCosmosClient(client, settings);
        });

        services.AddSingleton<ITraceCosmosClient, TraceCosmosClient>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var client = sp.GetRequiredService<CosmosClient>();
            var settings = sp.GetRequiredService<CosmosDbSettings>();
            settings.ContainerId = config["TraceDb:ContainerId"] ?? string.Empty;

            return new TraceCosmosClient(client, settings);
        });

        return services.AddSingleton<IClaimConverter, ClaimConverter>()
            .AddSingleton<IReclaimConverter, ReclaimConverter>()
            .AddSingleton<ICsvFileGenerator, CsvFileGenerator>()
            .AddSingleton<IEfinLookupCacheService, EfinLookupCacheService>();
    }
}
