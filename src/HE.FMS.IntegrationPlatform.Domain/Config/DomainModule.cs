using HE.FMS.IntegrationPlatform.Common.Extensions;
using HE.FMS.IntegrationPlatform.Contract.Common;
using HE.FMS.IntegrationPlatform.Contract.Grants.Results;
using HE.FMS.IntegrationPlatform.Contract.Grants.UseCases;
using HE.FMS.IntegrationPlatform.Domain.Framework;
using HE.FMS.IntegrationPlatform.Domain.Grants;
using HE.FMS.IntegrationPlatform.Domain.Grants.Mappers;
using HE.FMS.IntegrationPlatform.Domain.Grants.Services;
using HE.FMS.IntegrationPlatform.Domain.Grants.Settings;
using HE.FMS.IntegrationPlatform.Domain.Mambu;
using HE.FMS.IntegrationPlatform.Domain.PoC;
using Microsoft.Extensions.DependencyInjection;

namespace HE.FMS.IntegrationPlatform.Domain.Config;

public static class DomainModule
{
    public static IServiceCollection AddDomainModule(this IServiceCollection services)
    {
        services.AddScoped<IPoCService, PoCService>();

        return services.AddMambu()
            .AddGrants();
    }

    private static IServiceCollection AddMambu(this IServiceCollection services)
    {
        services.AddScoped<IMambuApiKeyService, MambuApiKeyService>();

        return services;
    }

    private static IServiceCollection AddGrants(this IServiceCollection services)
    {
        services.AddAppConfiguration<IGrantsSettings, GrantsSettings>("Grants");
        services.AddSingleton<IOrganisationMapper, OrganisationMapper>();
        services.AddScoped<IOrganisationService, OrganisationService>();
        services.AddScoped<IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult>, OpenNewGrantAccountUseCase>();

        return services;
    }
}
