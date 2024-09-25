using System.Text.Json.Serialization;

namespace HE.FMS.Middleware.Contract.Enums;
#pragma warning disable CA1712 // Do not prefix enum values with type name
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum VatCode
{
    VatCode02,
    VatCode03,
    VatCode04,
    VatCode10,
    VatCode11,
    VatCode12,
    VatCode13,
    VatCode20,
    VatCode21,
    VatCode22,
    VatCode98,
}
#pragma warning restore CA1712 // Do not prefix enum values with type name
