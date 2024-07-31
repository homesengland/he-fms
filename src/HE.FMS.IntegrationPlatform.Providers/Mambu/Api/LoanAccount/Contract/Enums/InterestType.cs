using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum InterestType
{
    [Description("SIMPLE_INTEREST")]
    SimpleInterest,

    [Description("CAPITALIZED_INTEREST")]
    CapitalizedInterest,

    [Description("COMPOUNDING_INTEREST")]
    CompoundingInterest,
}
