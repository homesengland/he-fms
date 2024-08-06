using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class PenaltySettingsDto
{
    public LoanPenaltyCalculationMethod? LoanPenaltyCalculationMethod { get; set; }

    public decimal? PenaltyRate { get; set; }
}
