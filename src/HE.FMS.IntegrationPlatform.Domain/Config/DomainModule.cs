using HE.FMS.IntegrationPlatform.Domain.Mambu;
using HE.FMS.IntegrationPlatform.Domain.PoC;
using Microsoft.Extensions.DependencyInjection;

namespace HE.FMS.IntegrationPlatform.Domain.Config;

public static class DomainModule
{
    public static IServiceCollection AddDomainModule(this IServiceCollection services)
    {
        services.AddScoped<IPoCService, PoCService>();
        services.AddScoped<IMambuService, MambuService>();
        services.AddScoped<IMambuApiKeyService, MambuApiKeyService>();

        return services;
    }
}
