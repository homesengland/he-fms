using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Constants;
using HE.FMS.Middleware.Contract.Enums;

namespace HE.FMS.Middleware.Contract.Claims;
public class ClaimDetailsBase
{
    [Required]
    public Milestone Milestone { get; set; }

    [Required]
    [MaxLength(ValidatorConstants.EfinIdLength)]
    public string InvoiceId { get; set; }
}
