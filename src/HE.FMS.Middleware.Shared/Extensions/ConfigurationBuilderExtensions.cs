using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace HE.FMS.Middleware.Extensions;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddFmsConfiguration(this IConfigurationBuilder builder, HostBuilderContext context)
    {
        return builder.SetBasePath(context.HostingEnvironment.ContentRootPath)
            .AddEnvironmentVariables()
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets(typeof(ConfigurationBuilderExtensions).Assembly, optional: true, reloadOnChange: true);
    }
}
