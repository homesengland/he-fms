using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace HE.FMS.IntegrationPlatform.Extensions;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddFmsConfiguration(this IConfigurationBuilder builder, HostBuilderContext context)
    {
        return builder.SetBasePath(context.HostingEnvironment.ContentRootPath)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    }
}
