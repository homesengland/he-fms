using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum RepaymentPeriodUnit
{
    [Description("DAYS")]
    Days,

    [Description("WEEKS")]
    Weeks,

    [Description("MONTHS")]
    Months,

    [Description("YEARS")]
    Years,
}
