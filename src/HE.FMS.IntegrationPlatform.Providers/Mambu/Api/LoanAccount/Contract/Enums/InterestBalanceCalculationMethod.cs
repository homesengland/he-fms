using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum InterestBalanceCalculationMethod
{
    [Description("ONLY_PRINCIPAL")]
    OnlyPrincipal,

    [Description("PRINCIPAL_AND_INTEREST")]
    PrincipalAndInterest,
}
