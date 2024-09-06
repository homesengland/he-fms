namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount;

public sealed class PeriodicPaymentDto
{
    public decimal Amount { get; set; }

    public string? EncodedKey { get; set; }

    public int ToInstallment { get; set; }
}
