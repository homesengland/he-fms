using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum InterestType
{
    [Description("SIMPLE_INTEREST")]
    SimpleInterest,

    [Description("CAPITALIZED_INTEREST")]
    CapitalizedInterest,

    [Description("COMPOUNDING_INTEREST")]
    CompoundingInterest,
}
