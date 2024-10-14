using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Providers.Common;
using HE.FMS.Middleware.Providers.Common.Settings;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;
using HE.FMS.Middleware.Providers.File;
using HE.FMS.Middleware.Providers.File.Settings;
using HE.FMS.Middleware.Providers.SeriveBus.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Constants = HE.FMS.Middleware.Common.Constants;

namespace HE.FMS.Middleware.Providers.Config;

public static class ProvidersModule
{
    public static IServiceCollection AddProvidersModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddCommon()
            .AddCosmosDb()
            .AddServiceBus(configuration)
            .AddStorage();
    }

    internal static IServiceCollection AddCommon(this IServiceCollection services)
    {
        return services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>()
            .AddSingleton<IFileWriter, FileShareWriter>()
            .AddSingleton<IEnvironmentValidator, EnvironmentValidator>()
            .AddAppConfiguration<MemoryCacheSettings>("MemoryCache")
            .AddSingleton(x => new AllowedEnvironmentSettings(x.GetRequiredService<IConfiguration>()["AllowedEnvironments"]!));
    }

    internal static IServiceCollection AddCosmosDb(this IServiceCollection services)
    {
        services.AddAppConfiguration<CosmosDbSettings>("CosmosDb");

        services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<CosmosDbSettings>();

            return !string.IsNullOrWhiteSpace(settings.AccountEndpoint)
                ? new CosmosClient(accountEndpoint: settings.AccountEndpoint, new DefaultAzureCredential())
                : new CosmosClient(connectionString: settings.ConnectionString);
        });

        services.AddHealthChecks().AddAzureCosmosDB();

        return services;
    }

    internal static IServiceCollection AddServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(sp => new ServiceBusSettings()
        {
            ClaimsTopic = configuration[Constants.Settings.ServiceBus.ClaimsTopic] ?? string.Empty,
            ReclaimsTopic = configuration[Constants.Settings.ServiceBus.ReclaimsTopic] ?? string.Empty,
        });

        services.AddAzureClients(clientBuilder =>
        {
            if (!string.IsNullOrWhiteSpace(configuration[Constants.Settings.ServiceBus.FullyQualifiedNamespace])
                && !string.IsNullOrWhiteSpace(configuration[Constants.Settings.ServiceBus.ConnectionString]))
            {
                clientBuilder.AddServiceBusClient(configuration[Constants.Settings.ServiceBus.ConnectionString])
                    .WithName(Constants.Settings.ServiceBus.DefaultClientName);
            }
            else if (!string.IsNullOrWhiteSpace(configuration[Constants.Settings.ServiceBus.FullyQualifiedNamespace])
                     && string.IsNullOrWhiteSpace(configuration[Constants.Settings.ServiceBus.ConnectionString]))
            {
                clientBuilder.AddServiceBusClientWithNamespace(configuration[Constants.Settings.ServiceBus.FullyQualifiedNamespace])
                    .WithCredential(new DefaultAzureCredential())
                    .WithName(Constants.Settings.ServiceBus.DefaultClientName);
            }
            else
            {
                throw new MissingConfigurationException(Constants.Settings.ServiceBus.ConnectionString);
            }
        });

        services.AddHealthChecks().AddAzureServiceBusTopic(
            sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return config[Constants.Settings.ServiceBus.FullyQualifiedNamespace] ?? string.Empty;
            },
            sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return config[Constants.Settings.ServiceBus.ClaimsTopic] ?? string.Empty;
            },
            _ => new ManagedIdentityCredential(),
            name: "claims_topic");

        services.AddHealthChecks().AddAzureServiceBusTopic(
            sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return config[Constants.Settings.ServiceBus.FullyQualifiedNamespace] ?? string.Empty;
            },
            sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return config[Constants.Settings.ServiceBus.ReclaimsTopic] ?? string.Empty;
            },
            _ => new ManagedIdentityCredential(),
            name: "reclaims_topic");

        return services;
    }

    internal static IServiceCollection AddStorage(this IServiceCollection services)
    {
        services.AddAppConfiguration<FileStorageSettings>("IntegrationStorage");

        services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<FileStorageSettings>();
            return new BlobServiceClient(settings.ConnectionString);
        });

        services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<FileStorageSettings>();
            return new ShareClient(settings.ConnectionString, settings.ShareName);
        });

        services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<FileStorageSettings>();
            return new ShareServiceClient(settings.ConnectionString);
        });

        services.AddHealthChecks().AddAzureFileShare();

        return services;
    }
}
