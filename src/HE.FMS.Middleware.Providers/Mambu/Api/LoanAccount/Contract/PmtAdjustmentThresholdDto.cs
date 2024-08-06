using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class PmtAdjustmentThresholdDto
{
    public PmtAdjustmentMethod? Method { get; set; }

    public int? NumberOfDays { get; set; }
}
