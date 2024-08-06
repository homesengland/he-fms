using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement.Contract.Enums;

public enum ExposureLimitType
{
    [Description("APPROVED_AMOUNT")]
    ApprovedAmount,

    [Description("OUTSTANDING_AMOUNT")]
    OutstandingAmount,
}
