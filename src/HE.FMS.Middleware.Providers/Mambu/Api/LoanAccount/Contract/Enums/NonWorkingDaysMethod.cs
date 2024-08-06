using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum NonWorkingDaysMethod
{
    [Description("INCLUDED")]
    Included,

    [Description("EXCLUDED")]
    Excluded,
}
