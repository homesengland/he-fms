using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.Common.Enums;

public enum AccountType
{
    [Description("LOAN")]
    Loan,

    [Description("DEPOSIT")]
    Deposit,
}
