using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum ScheduleDueDatesMethod
{
    [Description("INTERVAL")]
    Interval,

    [Description("FIXED_DAYS_OF_MONTH")]
    FixedDaysOfMonth,
}
