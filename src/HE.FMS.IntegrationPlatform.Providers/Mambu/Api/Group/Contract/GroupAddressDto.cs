namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group.Contract;

public sealed class GroupAddressDto
{
    public string? City { get; set; }

    public string? Country { get; set; }

    public string? EncodedKey { get; set; }

    public int? IndexInList { get; set; }

    public double? Latitude { get; set; }

    public string? Line1 { get; set; }

    public string? Line2 { get; set; }

    public double? Longitude { get; set; }

    public string? ParentKey { get; set; }

    public string? Postcode { get; set; }

    public string? Region { get; set; }
}
