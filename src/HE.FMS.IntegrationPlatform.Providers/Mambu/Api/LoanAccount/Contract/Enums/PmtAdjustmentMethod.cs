using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum PmtAdjustmentMethod
{
    [Description("WORKING_DAYS")]
    WorkingDays,

    [Description("CALENDAR_DAYS")]
    CalendarDays,
}
