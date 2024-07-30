using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum AccountState
{
    [Description("PARTIAL_APPLICATION")]
    PartialApplication,

    [Description("PENDING_APPROVAL")]
    PendingApproval,

    [Description("APPROVED")]
    Approved,

    [Description("ACTIVE")]
    Active,

    [Description("ACTIVE_IN_ARREARS")]
    ActiveInArrears,

    [Description("CLOSED")]
    Closed,
}
