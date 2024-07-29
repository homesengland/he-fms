using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement.Contract.Enums;

public enum CreditArrangementSubState
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
