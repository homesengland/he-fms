using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Claims;
public class ClaimDetails : ClaimDetailsBase
{
    [Required]
    [Range(0.01, int.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    [MaxLength(ValidatorConstants.CustomNameMaxLength)]
    public string ApprovedBy { get; set; }

    [Required]
    public DateTimeOffset ApprovedOn { get; set; }
}
