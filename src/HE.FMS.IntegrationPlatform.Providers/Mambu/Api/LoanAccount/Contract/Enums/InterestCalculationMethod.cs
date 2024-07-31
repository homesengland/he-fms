using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum InterestCalculationMethod
{
    [Description("FLAT")]
    Flat,

    [Description("DECLINING_BALANCE")]
    DecliningBalance,

    [Description("DECLINING_BALANCE_DISCOUNTED")]
    DecliningBalanceDiscounted,

    [Description("EQUAL_INSTALLMENTS")]
    EqualInstallments,
}
