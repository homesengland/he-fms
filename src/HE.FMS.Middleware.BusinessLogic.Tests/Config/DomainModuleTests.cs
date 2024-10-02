using Azure.Identity;
using HE.FMS.Middleware.BusinessLogic.Config;
using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
using HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
using HE.FMS.Middleware.BusinessLogic.Trace.CosmosDb;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Providers.Common;
using HE.FMS.Middleware.Providers.Common.Settings;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Config;
public class DomainModuleTests
{
    private readonly IServiceCollection _services;
    private readonly IConfiguration _configuration;

    private readonly Dictionary<string, string> _inMemorySettings = new Dictionary<string, string> {
        {"CosmosDb:AccountEndpoint", "https://test-cosmos-db.documents.azure.com:443/"},
        {"CosmosDb:ConnectionString", "AccountEndpoint=https://test-cosmos-db.documents.azure.com:443/;AccountKey=" },
        {"CosmosDb:DatabaseId", "testDatabase" },
        { "EfinDb:ContainerId", "efinContainer" },
        { "EfinConfigDb:ContainerId", "efinConfigContainer" },
        { "TraceDb:ContainerId", "traceContainer" },
    };

    public DomainModuleTests()
    {
        _services = new ServiceCollection();
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(_inMemorySettings!)
            .Build();
    }

    [Fact]
    public void AddClaimReclaim_ShouldRegisterClaimReclaimServices()
    {
        // Arrange
        _services.AddSingleton(_configuration);
        _services.AddMemoryCache();
        _services.AddAppConfiguration<MemoryCacheSettings>("MemoryCache");
        _services.AddAppConfiguration<CosmosDbSettings>("CosmosDb");
        _services.AddSingleton<IDateTimeProvider, FakeDateTimeProvider>();
        _services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<CosmosDbSettings>();
            return new CosmosClient(accountEndpoint: settings.AccountEndpoint, new DefaultAzureCredential());
        });

        // Act
        _services.AddClaimReclaim();
        var serviceProvider = _services.BuildServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider.GetService<IEfinCosmosClient>());
        Assert.NotNull(serviceProvider.GetService<IEfinIndexCosmosClient>());
        Assert.NotNull(serviceProvider.GetService<IEfinLookupCosmosClient>());
        Assert.NotNull(serviceProvider.GetService<ITraceCosmosClient>());
        Assert.NotNull(serviceProvider.GetService<IClaimConverter>());
        Assert.NotNull(serviceProvider.GetService<IReclaimConverter>());
        Assert.NotNull(serviceProvider.GetService<ICsvFileGenerator>());
        Assert.NotNull(serviceProvider.GetService<IEfinLookupCacheService>());
    }
}
