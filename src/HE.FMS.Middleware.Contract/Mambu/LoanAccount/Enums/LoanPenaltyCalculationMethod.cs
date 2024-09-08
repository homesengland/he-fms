using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

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
