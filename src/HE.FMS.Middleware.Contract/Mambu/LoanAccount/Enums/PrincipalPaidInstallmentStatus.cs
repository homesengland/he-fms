using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum PrincipalPaidInstallmentStatus
{
    [Description("PARTIALLY_PAID")]
    PartiallyPaid,

    [Description("PAID")]
    Paid,

    [Description("ORIGINAL_TOTAL_EXPECTED_PAID")]
    OriginalTotalExpectedPaid,
}
