using System.Text.Json;
using System.Text.Json.Serialization;
using HE.FMS.Middleware.Common.Config;
using HE.FMS.Middleware.Common.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Xunit;

namespace HE.FMS.Middleware.Common.Tests.Config;
public class CommonModuleTests
{
    private readonly IServiceCollection _services = new ServiceCollection();

    [Fact]
    public void AddCommonModule_ShouldRegisterAllServices()
    {
        // Act
        _services.AddSingleton<ILogger<StreamSerializer>, FakeLogger<StreamSerializer>>();
        _services.AddCommonModule();
        var serviceProvider = _services.BuildServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider.GetService<JsonSerializerOptions>());
        Assert.NotNull(serviceProvider.GetService<IStreamSerializer>());
        Assert.NotNull(serviceProvider.GetService<IObjectSerializer>());
    }

    [Fact]
    public void CommonSerializerOptions_ShouldHaveExpectedSettings()
    {
        // Act
        var options = CommonModule.CommonSerializerOptions;

        // Assert
        Assert.True(options.PropertyNameCaseInsensitive);
        Assert.Contains(options.Converters, converter => converter is JsonStringEnumConverter);
    }
}
