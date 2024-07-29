using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement.Contract.Enums;

public enum ExposureLimitType
{
    [Description("APPROVED_AMOUNT")]
    ApprovedAmount,

    [Description("OUTSTANDING_AMOUNT")]
    OutstandingAmount,
}
