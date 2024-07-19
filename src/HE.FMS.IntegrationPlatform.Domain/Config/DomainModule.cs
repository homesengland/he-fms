using HE.FMS.IntegrationPlatform.Domain.PoC;
using Microsoft.Extensions.DependencyInjection;

namespace HE.FMS.IntegrationPlatform.Domain.Config;

public static class DomainModule
{
    public static IServiceCollection AddDomainModule(this IServiceCollection services)
    {
        services.AddScoped<IPoCService, PoCService>();

        return services;
    }
}
