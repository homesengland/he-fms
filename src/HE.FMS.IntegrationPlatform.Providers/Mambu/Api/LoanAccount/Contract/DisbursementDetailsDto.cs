namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class DisbursementDetailsDto
{
    public DateTimeOffset? DisbursementDate { get; set; }

    public string? EncodedKey { get; set; }

    public DateTimeOffset? ExpectedDisbursementDate { get; set; }

    public IList<CustomPredefinedFeeDto>? Fees { get; set; }

    public DateTimeOffset? FirstRepaymentDate { get; set; }

    public LoanTransactionDetailsDto? TransactionDetails { get; set; }
}
