using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum FuturePaymentsAcceptanceType
{
    [Description("NO_FUTURE_PAYMENTS")]
    NoFuturePayments,

    [Description("ACCEPT_FUTURE_PAYMENTS")]
    AcceptFuturePayments,

    [Description("ACCEPT_OVERPAYMENTS")]
    AcceptOverpayments,
}
