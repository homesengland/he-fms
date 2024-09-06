using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.CreditArrangement.Enums;

public enum CreditArrangementState
{
    [Description("PENDING_APPROVAL")]
    PendingApproval,

    [Description("APPROVED")]
    Approved,

    [Description("ACTIVE")]
    Active,

    [Description("CLOSED")]
    Closed,

    [Description("WITHDRAWN")]
    Withdrawn,

    [Description("REJECTED")]
    Rejected,
}
