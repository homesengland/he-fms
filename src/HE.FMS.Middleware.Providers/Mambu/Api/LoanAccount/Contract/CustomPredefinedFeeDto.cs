namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class CustomPredefinedFeeDto
{
    public decimal Amount { get; set; }

    public string? EncodedKey { get; set; }

    public decimal? Percentage { get; set; }

    public string? PredefinedFeeEncodedKey { get; set; }
}
