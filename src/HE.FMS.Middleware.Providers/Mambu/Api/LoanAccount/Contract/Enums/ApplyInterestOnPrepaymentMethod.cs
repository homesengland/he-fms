using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum ApplyInterestOnPrepaymentMethod
{
    [Description("AUTOMATIC")]
    Automatic,

    [Description("MANUAL")]
    Manual,
}
