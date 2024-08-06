using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum LoanPenaltyCalculationMethod
{
    [Description("NONE")]
    None,

    [Description("OVERDUE_BALANCE")]
    OverdueBalance,

    [Description("OVERDUE_BALANCE_AND_INTEREST")]
    OverdueBalanceAndInterest,

    [Description("OUTSTANDING_PRINCIPAL")]
    OutstandingPrincipal,
}
