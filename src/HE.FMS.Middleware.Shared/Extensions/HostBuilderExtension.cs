using HE.FMS.Middleware.BusinessLogic.Config;
using HE.FMS.Middleware.Common.Config;
using HE.FMS.Middleware.Extensions;
using HE.FMS.Middleware.Providers.Config;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HE.FMS.Middleware.Shared.Extensions;

public static class HostBuilderExtension
{
    public static IHost SetupHostBuilder(this IHostBuilder b)
    {
        return b.ConfigureFunctionsWorkerDefaults(builder => builder.UseFmsMiddlewares())
            .ConfigureAppConfiguration((context, builder) => builder.AddFmsConfiguration(context))
            .ConfigureServices(services =>
            {
                services.AddApplicationInsightsTelemetryWorkerService();
                services.ConfigureFunctionsApplicationInsights();
                services.AddMemoryCache();
                services.AddCommonModule()
                    .AddDomainModule()
                    .AddProvidersModule();
            })
            .Build();
    }
}
