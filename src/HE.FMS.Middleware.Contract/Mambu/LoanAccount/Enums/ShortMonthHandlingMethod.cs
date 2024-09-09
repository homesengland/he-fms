using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum ShortMonthHandlingMethod
{
    [Description("LAST_DAY_IN_MONTH")]
    LastDayInMonth,

    [Description("FIRST_DAY_OF_NEXT_MONTH")]
    FirstDayOfNextMonth,
}
