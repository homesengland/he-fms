using HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount;

public sealed class PenaltySettingsDto
{
    public LoanPenaltyCalculationMethod? LoanPenaltyCalculationMethod { get; set; }

    public decimal? PenaltyRate { get; set; }
}
