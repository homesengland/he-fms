using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace HE.FMS.Middleware.Shared.Extensions;

[ExcludeFromCodeCoverage]
public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddFmsConfiguration<T>(this IConfigurationBuilder builder, HostBuilderContext context)
        where T : class
    {
        builder.SetBasePath(context.HostingEnvironment.ContentRootPath)
            .AddEnvironmentVariables()
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets(typeof(T).Assembly, optional: true, reloadOnChange: true);

        return builder;
    }
}
