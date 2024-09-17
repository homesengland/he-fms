using System.ComponentModel.DataAnnotations;

namespace HE.FMS.Middleware.Contract.Claims;
public class ClaimDetails : ClaimDetailsBase
{
    [Required]
    [Range(0.01, int.MaxValue)]
    public decimal Amount { get; set; }
}
