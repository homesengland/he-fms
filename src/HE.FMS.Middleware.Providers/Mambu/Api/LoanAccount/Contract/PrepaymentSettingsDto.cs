using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class PrepaymentSettingsDto
{
    public ApplyInterestOnPrepaymentMethod? ApplyInterestOnPrepaymentMethod { get; set; }

    public ElementsRecalculationMethod? ElementsRecalculationMethod { get; set; }

    public decimal? ErcFreeAllowanceAmount { get; set; }

    public decimal? ErcFreeAllowancePercentage { get; set; }

    public PrepaymentRecalculationMethod? PrepaymentRecalculationMethod { get; set; }

    public PrincipalPaidInstallmentStatus? PrincipalPaidInstallmentStatus { get; set; }
}
