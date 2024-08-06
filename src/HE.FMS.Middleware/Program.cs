using HE.FMS.Middleware.BusinessLogic.Config;
using HE.FMS.Middleware.Common.Config;
using HE.FMS.Middleware.Extensions;
using HE.FMS.Middleware.Providers.Config;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(builder => builder.UseFmsMiddlewares())
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

await host.RunAsync();
