using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum RepaymentScheduleMethod
{
    [Description("NONE")]
    None,

    [Description("FIXED")]
    Fixed,

    [Description("DYNAMIC")]
    Dynamic,
}
