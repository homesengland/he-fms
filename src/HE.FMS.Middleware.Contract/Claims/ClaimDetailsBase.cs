using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Claims;
public class ClaimDetailsBase
{
    [Required]
    [MaxLength(ValidatorConstants.EfinIdLength)]
    public string InvoiceId { get; set; }
}
