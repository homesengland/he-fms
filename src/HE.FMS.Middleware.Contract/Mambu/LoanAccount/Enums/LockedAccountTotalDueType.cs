using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum LockedAccountTotalDueType
{
    [Description("BALANCE_AMOUNT")]
    BalanceAmount,

    [Description("DUE_AMOUNT_ON_LATE_INSTALLMENTS")]
    DueAmountOnLateInstallments,
}
