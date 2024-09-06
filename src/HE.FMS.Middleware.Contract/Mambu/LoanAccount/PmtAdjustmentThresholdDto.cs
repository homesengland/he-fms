using HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount;

public sealed class PmtAdjustmentThresholdDto
{
    public PmtAdjustmentMethod? Method { get; set; }

    public int? NumberOfDays { get; set; }
}
