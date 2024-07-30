using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Contract;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract;

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
