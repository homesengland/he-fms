namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class LoanTransactionDetailsDto
{
    public string? EncodedKey { get; set; }

    public bool? InternalTransfer { get; set; }

    public string? TargetDepositAccountKey { get; set; }

    public string? TransactionChannelId { get; set; }

    public string? TransactionChannelKey { get; set; }
}
