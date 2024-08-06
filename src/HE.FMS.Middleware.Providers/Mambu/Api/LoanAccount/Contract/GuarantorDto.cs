using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class GuarantorDto
{
    public decimal Amount { get; set; }

    public string? AssetName { get; set; }

    public string? DepositAccountKey { get; set; }

    public string? EncodedKey { get; set; }

    public string GuarantorKey { get; set; }

    public GuarantorType GuarantorType { get; set; }
}
