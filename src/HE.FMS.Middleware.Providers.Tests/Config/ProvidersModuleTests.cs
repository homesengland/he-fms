using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Providers.Common;
using HE.FMS.Middleware.Providers.Config;
using HE.FMS.Middleware.Providers.File;
using HE.FMS.Middleware.Providers.ServiceBus;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HE.FMS.Middleware.Providers.Tests.Config;
public class ProvidersModuleTests
{
    private readonly IServiceCollection _services;
    private readonly IConfiguration _configuration;

    private readonly Dictionary<string, string> _inMemorySettings = new()
    {
        { "CosmosDb:AccountEndpoint", "https://test-cosmos-db.documents.azure.com:443/" },
        { "CosmosDb:ConnectionString", "AccountEndpoint=https://test-cosmos-db.documents.azure.com:443/;AccountKey=" },
        { "CosmosDb:DatabaseId", "testDatabase" },
        { Constants.Settings.ServiceBus.FullyQualifiedNamespace, "https://test-servicebus.servicebus.windows.net" },
        { Constants.Settings.ServiceBus.ClaimsTopic, "claims-topic" },
        { Constants.Settings.ServiceBus.ReclaimsTopic, "reclaims-topic" },
        { "IntegrationStorage:ConnectionString", "DefaultEndpointsProtocol=https;AccountName=mpgdevstorageaccount;AccountKey=;EndpointSuffix=core.windows.net" },
        { "IntegrationStorage:ShareName", "testShare" },
        { "AllowedEnvironments", "test" },
    };

    public ProvidersModuleTests()
    {
        _services = new ServiceCollection();
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(_inMemorySettings!)
            .Build();
    }

    [Fact]
    public void AddCommon_ShouldRegisterServices()
    {
        // Arrange
        _services.AddSingleton(_configuration);
        _services.AddMemoryCache();

        // Act
        _services.AddCommon();
        _services.AddStorage();
        var serviceProvider = _services.BuildServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider.GetService<IDateTimeProvider>());
        Assert.NotNull(serviceProvider.GetService<IFileWriter>());
        Assert.NotNull(serviceProvider.GetService<IEnvironmentValidator>());
    }

    [Fact]
    public void AddCosmosDb_ShouldRegisterServices()
    {
        // Arrange
        _services.AddSingleton(_configuration);

        // Act
        _services.AddCosmosDb();
        var serviceProvider = _services.BuildServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider.GetService<CosmosClient>());
    }

    [Fact]
    public void AddServiceBus_ShouldRegisterServices()
    {
        // Arrange
        _services.AddSingleton(_configuration);

        // Act
        _services.AddServiceBus();
        var serviceProvider = _services.BuildServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider.GetService<ITopicClientFactory>());
    }

    [Fact]
    public void AddStorage_ShouldRegisterServices()
    {
        // Arrange
        _services.AddSingleton(_configuration);

        // Act
        _services.AddStorage();
        var serviceProvider = _services.BuildServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider.GetService<BlobServiceClient>());
        Assert.NotNull(serviceProvider.GetService<ShareClient>());
        Assert.NotNull(serviceProvider.GetService<ShareServiceClient>());
    }
}
