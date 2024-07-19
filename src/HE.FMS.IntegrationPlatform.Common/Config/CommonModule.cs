using HE.FMS.IntegrationPlatform.Common.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace HE.FMS.IntegrationPlatform.Common.Config;

public static class CommonModule
{
    public static IServiceCollection AddCommonModule(this IServiceCollection services)
    {
        services.AddSingleton<IBinarySerializer, BinarySerializer>();

        return services;
    }
}
