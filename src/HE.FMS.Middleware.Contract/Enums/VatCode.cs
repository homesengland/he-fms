using System.Text.Json.Serialization;

namespace HE.FMS.Middleware.Contract.Enums;
#pragma warning disable CA1712 // Do not prefix enum values with type name
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum VatCode
{
    VatCode02 = 2,
    VatCode03 = 3,
    VatCode04 = 4,
    VatCode10 = 10,
    VatCode11 = 11,
    VatCode12 = 12,
    VatCode13 = 13,
    VatCode20 = 20,
    VatCode21 = 21,
    VatCode22 = 22,
    VatCode98 = 98,
}
#pragma warning restore CA1712 // Do not prefix enum values with type name
