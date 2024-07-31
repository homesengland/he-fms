using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum PrincipalPaidInstallmentStatus
{
    [Description("PARTIALLY_PAID")]
    PartiallyPaid,

    [Description("PAID")]
    Paid,

    [Description("ORIGINAL_TOTAL_EXPECTED_PAID")]
    OriginalTotalExpectedPaid,
}
