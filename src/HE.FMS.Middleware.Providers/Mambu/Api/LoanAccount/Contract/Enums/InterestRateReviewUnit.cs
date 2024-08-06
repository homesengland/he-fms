using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum InterestRateReviewUnit
{
    [Description("DAYS")]
    Days,

    [Description("WEEKS")]
    Weeks,

    [Description("MONTHS")]
    Months,
}
