using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum InterestBalanceCalculationMethod
{
    [Description("ONLY_PRINCIPAL")]
    OnlyPrincipal,

    [Description("PRINCIPAL_AND_INTEREST")]
    PrincipalAndInterest,
}
