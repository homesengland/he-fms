using System.ComponentModel.DataAnnotations;

namespace HE.FMS.Middleware.Contract.Common;

public sealed class OrganisationContract
{
    [Required]
    [MaxLength(32)]
    public string Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [MaxLength(255)]
    public string? PhoneNumber { get; set; }

    [MaxLength(255)]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public AddressContract Address { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }
}
