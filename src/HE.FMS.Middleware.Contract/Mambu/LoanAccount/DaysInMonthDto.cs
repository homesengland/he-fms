using HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount;

public sealed class DaysInMonthDto
{
    public int? DaysInMonth { get; set; }

    public ShortMonthHandlingMethod? ShortMonthHandlingMethod { get; set; }
}
