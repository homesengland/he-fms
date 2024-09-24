using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Constants;
using HE.FMS.Middleware.Contract.Enums;

namespace HE.FMS.Middleware.Contract.Claims;
public class ClaimDetailsBase
{
    [Required]
    [MaxLength(ValidatorConstants.IdMaxLength)]
    public string Id { get; set; }

    [Required]
    public Milestone Milestone { get; set; }
}
