using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum GracePeriodType
{
    [Description("NONE")]
    None,

    [Description("PAY_INTEREST_ONLY")]
    PayInterestOnly,

    [Description("INTEREST_FORGIVENESS")]
    InterestForgiveness,
}
