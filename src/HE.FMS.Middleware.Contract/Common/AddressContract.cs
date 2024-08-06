using System.ComponentModel.DataAnnotations;

namespace HE.FMS.Middleware.Contract.Common;

public sealed class AddressContract
{
    [MaxLength(255)]
    public string? Line1 { get; set; }

    [MaxLength(255)]
    public string? Line2 { get; set; }

    [MaxLength(255)]
    public string? City { get; set; }

    [MaxLength(255)]
    public string? Country { get; set; }
}
