using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum LatePaymentsRecalculationMethod
{
    [Description("OVERDUE_INSTALLMENTS_INCREASE")]
    OverdueInstallmentsIncrease,

    [Description("LAST_INSTALLMENT_INCREASE")]
    LastInstallmentIncrease,

    [Description("NO_RECALCULATION")]
    NoRecalculation,
}
