using HE.FMS.Middleware.Contract.Mambu.Common.Contract;
using HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount;

public sealed class AssetDto
{
    public decimal Amount { get; set; }

    public string? EncodedKey { get; set; }

    public string AssetName { get; set; }

    public string? DepositAccountKey { get; set; }

    public GuarantorType? GuarantorKey { get; set; }

    public GuarantorType? GuarantorType { get; set; }

    public decimal? OriginalAmount { get; set; }

    public CurrencyDto? OriginalCurrency { get; set; }
}
