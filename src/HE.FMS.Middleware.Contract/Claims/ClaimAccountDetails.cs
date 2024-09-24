using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Enums;

namespace HE.FMS.Middleware.Contract.Claims;
public sealed class ClaimAccountDetails
{
    [Required]
    [MaxLength(Constants.ValidatorConstants.CustomNameMaxLength)]
    public string Name { get; set; }

    [Required]
    public PartnerType PartnerType { get; set; }
}
