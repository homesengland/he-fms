using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Claims;

namespace HE.FMS.Middleware.Contract.Reclaims;
public sealed class ReclaimDetails : ClaimDetailsBase
{
    [Required]
    [Range(0.01, int.MaxValue)]
    public decimal TotalAmount { get; set; }

    [Required]
    [Range(0.01, int.MaxValue)]
    public decimal InterestAmount { get; set; }
}
