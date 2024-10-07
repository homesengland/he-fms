using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Tests.Fakes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HE.FMS.Middleware.Common.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddAppConfiguration_WithServiceAndImplementation_ShouldRegisterSingleton()
    {
        // Arrange  
        var services = new ServiceCollection();
        var configurationKey = "FakeConfig";

        var inMemorySettings = new Dictionary<string, string> { { "FakeConfig:Setting", "TestValue" }, };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        services.AddSingleton(configuration);

        // Act  
        services.AddAppConfiguration<IFakeConfig, FakeConfig>(configurationKey);

        var provider = services.BuildServiceProvider();
        var service = provider.GetService<IFakeConfig>();

        // Assert  
        Assert.NotNull(service);
        Assert.IsType<FakeConfig>(service);
        Assert.Equal("TestValue", (service as FakeConfig)?.Setting);
    }

    [Fact]
    public void AddAppConfiguration_WithImplementationOnly_ShouldRegisterSingleton()
    {
        // Arrange  
        var services = new ServiceCollection();
        var configurationKey = "FakeConfig";

        var inMemorySettings = new Dictionary<string, string> { { "FakeConfig:Setting", "TestValue" }, };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        services.AddSingleton(configuration);

        // Act  
        services.AddAppConfiguration<FakeConfig>(configurationKey);

        var provider = services.BuildServiceProvider();
        var service = provider.GetService<FakeConfig>();

        // Assert  
        Assert.NotNull(service);
        Assert.Equal("TestValue", service.Setting);
    }
}
