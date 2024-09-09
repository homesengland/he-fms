namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount;

public sealed class CustomPredefinedFeeDto
{
    public decimal Amount { get; set; }

    public string? EncodedKey { get; set; }

    public decimal? Percentage { get; set; }

    public string? PredefinedFeeEncodedKey { get; set; }
}
