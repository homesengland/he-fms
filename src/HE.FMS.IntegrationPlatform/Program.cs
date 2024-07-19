using HE.FMS.IntegrationPlatform.Common.Config;
using HE.FMS.IntegrationPlatform.Domain.Config;
using HE.FMS.IntegrationPlatform.Extensions;
using HE.FMS.IntegrationPlatform.Providers.Config;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(builder => builder.UseCommonMiddlewares())
    .ConfigureAppConfiguration((context, builder) => builder.AddDefaultConfiguration(context))
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddCommonModule()
            .AddDomainModule()
            .AddProvidersModule();
    })
    .Build();

await host.RunAsync();
