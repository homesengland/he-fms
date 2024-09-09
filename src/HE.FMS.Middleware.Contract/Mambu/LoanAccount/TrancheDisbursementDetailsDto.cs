namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount;

public sealed class TrancheDisbursementDetailsDto
{
    public string? DisbursementTransactionKey { get; set; }

    public DateTimeOffset? ExpectedDisbursementDate { get; set; }
}
