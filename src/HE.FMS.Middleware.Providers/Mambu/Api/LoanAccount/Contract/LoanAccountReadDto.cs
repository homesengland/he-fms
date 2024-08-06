namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class LoanAccountReadDto : LoanAccountDto
{
    public string EncodedKey { get; set; }

    public int? DaysInArrears { get; set; }

    public int? DaysLate { get; set; }

    public DateTimeOffset CreationDate { get; set; }

    public DateTimeOffset? ApprovedDate { get; set; }

    public DateTimeOffset? ClosedDate { get; set; }

    public DateTimeOffset? LastAccountAppraisalDate { get; set; }

    public DateTimeOffset? LastInterestAppliedDate { get; set; }

    public DateTimeOffset? LastInterestReviewDate { get; set; }

    public DateTimeOffset? LastLockedDate { get; set; }

    public DateTimeOffset? LastModifiedDate { get; set; }

    public DateTimeOffset? LastSetToArrearsDate { get; set; }

    public DateTimeOffset? LastTaxRateReviewDate { get; set; }

    public double? InterestAccruedInBillingCycle { get; set; }

    public double? InterestFromArrearsAccrued { get; set; }

    public bool? ModifyInterestForFirstInstallment { get; set; }
}
