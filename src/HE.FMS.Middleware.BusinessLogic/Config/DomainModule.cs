using HE.FMS.Middleware.BusinessLogic.Framework;
using HE.FMS.Middleware.BusinessLogic.Grants;
using HE.FMS.Middleware.BusinessLogic.Grants.Services;
using HE.FMS.Middleware.BusinessLogic.Grants.Settings;
using HE.FMS.Middleware.BusinessLogic.Mambu;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Grants.Results;
using HE.FMS.Middleware.Contract.Grants.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace HE.FMS.Middleware.BusinessLogic.Config;

public static class DomainModule
{
    public static IServiceCollection AddDomainModule(this IServiceCollection services)
    {
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
        services.AddScoped<ILoanAccountService, LoanAccountService>();
        services.AddScoped<IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult>, OpenNewGrantAccountUseCase>();

        return services;
    }
}
