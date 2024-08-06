namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class TrancheDisbursementDetailsDto
{
    public string? DisbursementTransactionKey { get; set; }

    public DateTimeOffset? ExpectedDisbursementDate { get; set; }
}
