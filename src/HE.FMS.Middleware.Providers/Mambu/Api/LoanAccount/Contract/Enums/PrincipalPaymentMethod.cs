using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum PrincipalPaymentMethod
{
    [Description("FLAT")]
    Flat,

    [Description("OUTSTANDING_PRINCIPAL_PERCENTAGE")]
    OutstandingPrincipalPercentage,

    [Description("PRINCIPAL_PERCENTAGE_LAST_DISB")]
    PrincipalPercentageLastDisb,

    [Description("TOTAL_BALANCE_PERCENTAGE")]
    TotalBalancePercentage,

    [Description("TOTAL_BALANCE_FLAT")]
    TotalBalanceFlat,

    [Description("TOTAL_PRINCIPAL_PERCENTAGE")]
    TotalPrincipalPercentage,
}
