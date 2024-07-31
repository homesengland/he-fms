using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class PmtAdjustmentThresholdDto
{
    public PmtAdjustmentMethod? Method { get; set; }

    public int? NumberOfDays { get; set; }
}
