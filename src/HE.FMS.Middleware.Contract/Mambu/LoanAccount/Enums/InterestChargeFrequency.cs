using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum InterestChargeFrequency
{
    [Description("ANNUALIZED")]
    Annualized,

    [Description("EVERY_MONTH")]
    EveryMonth,

    [Description("EVERY_FOUR_WEEKS")]
    EveryFourWeeks,

    [Description("EVERY_WEEK")]
    EveryWeek,

    [Description("EVERY_DAY")]
    EveryDay,

    [Description("EVERY_X_DAYS")]
    EveryXDays,
}
