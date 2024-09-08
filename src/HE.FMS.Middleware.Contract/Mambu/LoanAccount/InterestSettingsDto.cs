using HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount;

public sealed class InterestSettingsDto
{
    public IList<AccountInterestRateSettingsDto>? AccountInterestRateSettings { get; set; }

    public bool? AccrueInterestAfterMaturity { get; set; }

    public bool? AccrueLateInterest { get; set; }

    public DaysInMonthDto? InterestApplicationDays { get; set; }

    public InterestApplicationMethod? InterestApplicationMethod { get; set; }

    public InterestBalanceCalculationMethod? InterestBalanceCalculationMethod { get; set; }

    public InterestCalculationMethod? InterestCalculationMethod { get; set; }

    public InterestChargeFrequency? InterestChargeFrequency { get; set; }

    public decimal? InterestRate { get; set; }

    public int? InterestRateReviewCount { get; set; }

    public InterestRateReviewUnit? InterestRateReviewUnit { get; set; }

    public InterestRateSource? InterestRateSource { get; set; }

    public decimal? InterestSpread { get; set; }

    public InterestType? InterestType { get; set; }

    public PmtAdjustmentThresholdDto? PmtAdjustmentThreshold { get; set; }
}
