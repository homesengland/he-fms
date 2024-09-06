using HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount;

public sealed class PrincipalPaymentAccountSettingsDto
{
    public decimal? Amount { get; set; }

    public string? EncodedKey { get; set; }

    public bool? IncludeFeesInFloorAmount { get; set; }

    public bool? IncludeInterestInFloorAmount { get; set; }

    public decimal? Percentage { get; set; }

    public decimal? PrincipalCeilingValue { get; set; }

    public decimal? PrincipalFloorValue { get; set; }

    public PrincipalPaymentMethod? PrincipalPaymentMethod { get; set; }

    public decimal? TotalDueAmountFloor { get; set; }

    public TotalDuePayment? TotalDuePayment { get; set; }
}
