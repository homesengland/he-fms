using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Enums;

namespace HE.FMS.Middleware.Contract.Reclaims;
public sealed class ReclaimDetails : ClaimDetailsBase
{
    public Milestone? Milestone { get; set; }

    [Required]
    [Range(0.01, int.MaxValue)]
    public decimal TotalAmount { get; set; }

    [Range(0, int.MaxValue)]
    public decimal Amount { get; set; }

    [Range(0, int.MaxValue)]
    public decimal InterestAmount { get; set; }
}
