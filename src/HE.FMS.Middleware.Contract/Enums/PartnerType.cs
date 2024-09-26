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
    Landowner,
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
