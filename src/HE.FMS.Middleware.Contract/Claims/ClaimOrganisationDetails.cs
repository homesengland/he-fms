using System.ComponentModel.DataAnnotations;

namespace HE.FMS.Middleware.Contract.Claims;
public sealed class ClaimOrganisationDetails
{
    [Required]
    [MaxLength(Constants.ValidatorConstants.CustomNameMaxLength)]
    public string Name { get; set; }

    [Required]
    [MaxLength(Constants.ValidatorConstants.EnumMaxLength)]
    public string PartnerType { get; set; }
}
