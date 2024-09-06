namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount;

public sealed class LoanTransactionDetailsDto
{
    public string? EncodedKey { get; set; }

    public bool? InternalTransfer { get; set; }

    public string? TargetDepositAccountKey { get; set; }

    public string? TransactionChannelId { get; set; }

    public string? TransactionChannelKey { get; set; }
}
