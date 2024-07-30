﻿namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class PlannedInstallmentFeeDto
{
    public decimal? Amount { get; set; }

    public DateTimeOffset? ApplyOnDate { get; set; }

    public string? EncodedKey { get; set; }

    public string? InstallmentKey { get; set; }

    public int? InstallmentNumber { get; set; }

    public string PredefinedFeeKey { get; set; }
}
