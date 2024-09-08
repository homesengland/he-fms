using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum ScheduleDueDatesMethod
{
    [Description("INTERVAL")]
    Interval,

    [Description("FIXED_DAYS_OF_MONTH")]
    FixedDaysOfMonth,
}
