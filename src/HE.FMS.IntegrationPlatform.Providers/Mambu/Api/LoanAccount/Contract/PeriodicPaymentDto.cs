namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class PeriodicPaymentDto
{
    public decimal Amount { get; set; }

    public string? EncodedKey { get; set; }

    public int ToInstallment { get; set; }
}
