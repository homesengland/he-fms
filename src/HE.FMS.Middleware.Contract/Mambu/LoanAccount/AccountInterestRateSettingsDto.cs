using HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount;

public sealed class AccountInterestRateSettingsDto
{
    public string? EncodedKey { get; set; }

    public string? IndexSourceKey { get; set; }

    public decimal? InterestRate { get; set; }

    public decimal? InterestRateCeilingValue { get; set; }

    public decimal? InterestRateFloorValue { get; set; }

    public int? InterestRateReviewCount { get; set; }

    public InterestRateReviewUnit? InterestRateReviewUnit { get; set; }

    public InterestRateSource InterestRateSource { get; set; }

    public decimal? InterestSpread { get; set; }

    public DateTimeOffset ValidFrom { get; set; }
}
