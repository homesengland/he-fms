using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class DaysInMonthDto
{
    public int? DaysInMonth { get; set; }

    public ShortMonthHandlingMethod? ShortMonthHandlingMethod { get; set; }
}
