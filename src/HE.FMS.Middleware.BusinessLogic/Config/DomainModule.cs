using System.Diagnostics.CodeAnalysis;
using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
using HE.FMS.Middleware.BusinessLogic.Framework;
using HE.FMS.Middleware.BusinessLogic.Grants;
using HE.FMS.Middleware.BusinessLogic.Grants.Services;
using HE.FMS.Middleware.BusinessLogic.Grants.Settings;
using HE.FMS.Middleware.BusinessLogic.Mambu;
using HE.FMS.Middleware.BusinessLogic.Trace.CosmosDb;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Grants.Results;
using HE.FMS.Middleware.Contract.Grants.UseCases;
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

#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable S1144 // Unused private types or members should be removed
    [ExcludeFromCodeCoverage]
    private static IServiceCollection AddMambu(this IServiceCollection services)
    {
        services.AddScoped<IMambuApiKeyService, MambuApiKeyService>();

        return services;
    }

    [ExcludeFromCodeCoverage]
    private static IServiceCollection AddGrants(this IServiceCollection services)
    {
        services.AddAppConfiguration<IGrantsSettings, GrantsSettings>("Grants");
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<ICreditArrangementService, CreditArrangementService>();
        services.AddScoped<ILoanAccountService, LoanAccountService>();
        services.AddScoped<IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult>, OpenNewGrantAccountUseCase>();

        return services;
    }
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning restore S1144 // Unused private types or members should be removed
}
