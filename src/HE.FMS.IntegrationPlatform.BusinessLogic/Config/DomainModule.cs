using HE.FMS.IntegrationPlatform.BusinessLogic.Framework;
using HE.FMS.IntegrationPlatform.BusinessLogic.Grants;
using HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Services;
using HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Settings;
using HE.FMS.IntegrationPlatform.BusinessLogic.Mambu;
using HE.FMS.IntegrationPlatform.BusinessLogic.PoC;
using HE.FMS.IntegrationPlatform.Common.Extensions;
using HE.FMS.IntegrationPlatform.Contract.Grants.Results;
using HE.FMS.IntegrationPlatform.Contract.Grants.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace HE.FMS.IntegrationPlatform.BusinessLogic.Config;

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
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<ICreditArrangementService, CreditArrangementService>();
        services.AddScoped<IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult>, OpenNewGrantAccountUseCase>();

        return services;
    }
}
