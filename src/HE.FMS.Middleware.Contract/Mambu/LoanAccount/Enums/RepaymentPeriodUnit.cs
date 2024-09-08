using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum RepaymentPeriodUnit
{
    [Description("DAYS")]
    Days,

    [Description("WEEKS")]
    Weeks,

    [Description("MONTHS")]
    Months,

    [Description("YEARS")]
    Years,
}
