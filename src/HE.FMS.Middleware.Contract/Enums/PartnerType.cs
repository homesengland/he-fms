using System.Text.Json.Serialization;

namespace HE.FMS.Middleware.Contract.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PartnerType
{
    ArmsLengthBodyOfGovernment,
    ArmsLengthManagementOrganisation,
    Bank,
    CombinedAuthority,
    Consultant,
    Education,
    FinancialInstitution,
    ForProfitRegisteredProvider,
    GovernmentPolicyMaker,
    HealthAndSocialCare,
    Insurer,
    InvestmentManager,
    Landowner_134370003,
    Landowner_134370023,
    LocalAuthority,
    NonBankLender,
    NonRegisteredCharitableOrganisation,
    NotForProfitRegisteredProvider,
    Other,
    OtherFinancialInstitutionOrAdvisory,
    PrivateContractor,
    PrivateSectorHouseBuilder,
    PublicPrivatePartnership,
    RegisteredCharitableOrganisation,
    TradeAssociation,
    UnregisteredHousingAssociation,
}
