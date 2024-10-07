using System.Diagnostics.CodeAnalysis;
using HE.FMS.Middleware.BusinessLogic.Config;
using HE.FMS.Middleware.Common.Config;
using HE.FMS.Middleware.Providers.Config;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HE.FMS.Middleware.Shared.Extensions;

[ExcludeFromCodeCoverage]
public static class HostBuilderExtension
{
    public static IHost SetupHostBuilder<T>(this IHostBuilder hostBuilder)
        where T : class
    {
        return hostBuilder.ConfigureFunctionsWorkerDefaults(builder => builder.UseFmsMiddlewares())
            .ConfigureAppConfiguration((context, builder) => builder.AddFmsConfiguration<T>(context))
            .ConfigureServices(services =>
            {
                services.AddApplicationInsightsTelemetryWorkerService();
                services.ConfigureFunctionsApplicationInsights();
                services.AddMemoryCache();
                services.AddCommonModule()
                    .AddDomainModule()
                    .AddProvidersModule();
                services.AddHealthChecks();
            })
            .Build();
    }
}
