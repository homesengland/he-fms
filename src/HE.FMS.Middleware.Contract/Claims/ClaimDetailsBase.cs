using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Claims;
public class ClaimDetailsBase
{
    [Required]
    [MaxLength(ValidatorConstants.IdMaxLength)]
    public string Id { get; set; }

    [Required]
    [MaxLength(ValidatorConstants.EnumMaxLength)]
    public string Milestone { get; set; }

    [Required]
    [MaxLength(ValidatorConstants.CustomNameMaxLength)]
    public string ApprovedBy { get; set; }

    [Required]
    public DateTimeOffset ApprovedOn { get; set; }
}
