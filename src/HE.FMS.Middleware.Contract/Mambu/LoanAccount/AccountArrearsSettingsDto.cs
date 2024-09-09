using HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount;

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
