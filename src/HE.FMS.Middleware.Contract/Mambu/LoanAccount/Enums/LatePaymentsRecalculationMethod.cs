using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum LatePaymentsRecalculationMethod
{
    [Description("OVERDUE_INSTALLMENTS_INCREASE")]
    OverdueInstallmentsIncrease,

    [Description("LAST_INSTALLMENT_INCREASE")]
    LastInstallmentIncrease,

    [Description("NO_RECALCULATION")]
    NoRecalculation,
}
