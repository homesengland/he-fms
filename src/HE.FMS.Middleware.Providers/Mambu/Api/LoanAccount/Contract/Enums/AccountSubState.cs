using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum AccountSubState
{
    [Description("PARTIALLY_DISBURSED")]
    PartiallyDisbursed,

    [Description("LOCKED")]
    Locked,

    [Description("LOCKED_CAPPING")]
    LockedCapping,

    [Description("REFINANCED")]
    Refinanced,

    [Description("RESCHEDULED")]
    Rescheduled,

    [Description("WITHDRAWN")]
    Withdrawn,

    [Description("REPAID")]
    Repaid,

    [Description("REJECTED")]
    Rejected,

    [Description("WRITTEN_OFF")]
    WrittenOff,

    [Description("TERMINATED")]
    Terminated,
}
