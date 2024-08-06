using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum ElementsRecalculationMethod
{
    [Description("PRINCIPAL_EXPECTED_FIXED")]
    PrincipalExpectedFixed,

    [Description("TOTAL_EXPECTED_FIXED")]
    TotalExpectedFixed,
}
