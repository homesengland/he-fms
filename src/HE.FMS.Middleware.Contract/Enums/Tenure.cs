using System.Text.Json.Serialization;

namespace HE.FMS.Middleware.Contract.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Tenure
{
    AffordableRent,
    HomeOwnershipForPeopleWithLongTermDisabilities,
    OlderPersonsSharedOwnership,
    RentToBuy,
    SharedOwnership,
    SocialRent,
}
