using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum ElementsRecalculationMethod
{
    [Description("PRINCIPAL_EXPECTED_FIXED")]
    PrincipalExpectedFixed,

    [Description("TOTAL_EXPECTED_FIXED")]
    TotalExpectedFixed,
}
