﻿namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class LoanTrancheDto
{
    public decimal Amount { get; set; }

    public TrancheDisbursementDetailsDto? DisbursementDetails { get; set; }

    public string? EncodedKey { get; set; }

    public IList<CustomPredefinedFeeDto>? Fees { get; set; }

    public int? TrancheNumber { get; set; }
}
