using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.CreditArrangement.Enums;

public enum ExposureLimitType
{
    [Description("APPROVED_AMOUNT")]
    ApprovedAmount,

    [Description("OUTSTANDING_AMOUNT")]
    OutstandingAmount,
}
