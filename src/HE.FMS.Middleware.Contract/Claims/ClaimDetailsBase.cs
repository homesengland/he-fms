using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Claims;

public class ClaimDetailsBase
{
    [Required]
    [MaxLength(ValidatorConstants.IdMaxLength)]
    public string Id { get; set; }

    [Required]
    [Range(0.01, int.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    [MaxLength(ValidatorConstants.EnumMaxLength)]
    public string Milestone { get; set; }

    public string EfinMilestoneName =>
        Milestone switch
        {
            nameof(EfinConstants.Milestone.Acquisition) => EfinConstants.Milestone.Acquisition,
            nameof(EfinConstants.Milestone.StartOnSite) => EfinConstants.Milestone.StartOnSite,
            nameof(EfinConstants.Milestone.PracticalCompletion) => EfinConstants.Milestone.PracticalCompletion,
            _ => string.Empty,
        };

    [Required]
    [MaxLength(ValidatorConstants.CustomNameMaxLength)]
    public string AuthorisedBy { get; set; }

    [Required]
    public DateTimeOffset AuthorisedOn { get; set; }
}
