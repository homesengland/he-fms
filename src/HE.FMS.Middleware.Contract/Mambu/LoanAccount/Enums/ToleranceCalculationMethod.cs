using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum ToleranceCalculationMethod
{
    [Description("ARREARS_TOLERANCE_PERIOD")]
    ArrearsTolerancePeriod,

    [Description("MONTHLY_ARREARS_TOLERANCE_DAY")]
    MonthlyArrearsToleranceDay,
}
