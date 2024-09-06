using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum GracePeriodType
{
    [Description("NONE")]
    None,

    [Description("PAY_INTEREST_ONLY")]
    PayInterestOnly,

    [Description("INTEREST_FORGIVENESS")]
    InterestForgiveness,
}
