using System.ComponentModel.DataAnnotations;

namespace HE.FMS.Middleware.Contract.Claims;

public sealed class ClaimDetails
{
    [Required]
    [MaxLength(Constants.ValidatorConstants.IdMaxLength)]
    public string Id { get; set; }

    [Required]
    [Range(0.01, int.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    [MaxLength(Constants.ValidatorConstants.EnumMaxLength)]
    public string Milestone { get; set; }

    [Required]
    [MaxLength(Constants.ValidatorConstants.CustomNameMaxLength)]
    public string AuthorisedBy { get; set; }

    [Required]
    public DateTimeOffset AuthorisedOn { get; set; }
}
