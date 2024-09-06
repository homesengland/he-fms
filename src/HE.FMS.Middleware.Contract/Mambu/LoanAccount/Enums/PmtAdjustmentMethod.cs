using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum PmtAdjustmentMethod
{
    [Description("WORKING_DAYS")]
    WorkingDays,

    [Description("CALENDAR_DAYS")]
    CalendarDays,
}
