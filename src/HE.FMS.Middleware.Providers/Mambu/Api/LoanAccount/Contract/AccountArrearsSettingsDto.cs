using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class AccountArrearsSettingsDto
{
    public DateCalculationMethod? DateCalculationMethod { get; set; }

    public string? EncodedKey { get; set; }

    public int? MonthlyToleranceDay { get; set; }

    public NonWorkingDaysMethod? NonWorkingDaysMethod { get; set; }

    public ToleranceCalculationMethod? ToleranceCalculationMethod { get; set; }

    public decimal? ToleranceFloorAmount { get; set; }

    public decimal? TolerancePercentageOfOutstandingPrincipal { get; set; }

    public int? TolerancePeriod { get; set; }
}
