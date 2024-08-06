using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HE.FMS.Middleware.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppConfiguration<TService, TImplementation>(this IServiceCollection services, string configurationKey)
        where TService : class
        where TImplementation : class, TService
    {
        return services.AddSingleton<TService, TImplementation>(x =>
            x.GetRequiredService<IConfiguration>().GetSection(configurationKey).Get<TImplementation>()!);
    }

    public static IServiceCollection AddAppConfiguration<TImplementation>(this IServiceCollection services, string configurationKey)
        where TImplementation : class
    {
        return services.AddSingleton(x =>
            x.GetRequiredService<IConfiguration>().GetSection(configurationKey).Get<TImplementation>()!);
    }
}
