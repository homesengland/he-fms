using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum InterestRateSource
{
    [Description("FIXED_INTEREST_RATE")]
    FixedInterestRate,

    [Description("INDEX_INTEREST_RATE")]
    IndexInterestRate,
}
