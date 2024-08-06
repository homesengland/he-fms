using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum ToleranceCalculationMethod
{
    [Description("ARREARS_TOLERANCE_PERIOD")]
    ArrearsTolerancePeriod,

    [Description("MONTHLY_ARREARS_TOLERANCE_DAY")]
    MonthlyArrearsToleranceDay,
}
