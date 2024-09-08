using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum RepaymentScheduleMethod
{
    [Description("NONE")]
    None,

    [Description("FIXED")]
    Fixed,

    [Description("DYNAMIC")]
    Dynamic,
}
