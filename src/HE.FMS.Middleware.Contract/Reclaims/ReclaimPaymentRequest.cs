using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Claims;

namespace HE.FMS.Middleware.Contract.Reclaims;
public sealed class ReclaimPaymentRequest
{
    [Required]
    public ReclaimDetails Reclaim { get; set; }

    [Required]
    public ClaimApplicationDetails Application { get; set; }

    [Required]
    public ClaimOrganisationDetails Organisation { get; set; }
}
