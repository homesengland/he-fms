using HE.FMS.Middleware.Common.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace HE.FMS.Middleware.Common.Config;

public static class CommonModule
{
    public static IServiceCollection AddCommonModule(this IServiceCollection services)
    {
        services.AddSingleton<IStreamSerializer, StreamSerializer>();

        return services;
    }
}
