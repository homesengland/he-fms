using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum ApplyInterestOnPrepaymentMethod
{
    [Description("AUTOMATIC")]
    Automatic,

    [Description("MANUAL")]
    Manual,
}
