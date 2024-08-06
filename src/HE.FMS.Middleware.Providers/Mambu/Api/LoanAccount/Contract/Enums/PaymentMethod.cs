using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum PaymentMethod
{
    [Description("HORIZONTAL")]
    Horizontal,

    [Description("VERTICAL")]
    Vertical,
}
