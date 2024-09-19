using System.ComponentModel.DataAnnotations;

namespace HE.FMS.Middleware.Contract.Claims;
public sealed class ClaimPaymentRequest
{
    [Required]
    public ClaimDetails Claim { get; set; }

    [Required]
    public ClaimApplicationDetails Application { get; set; }

    [Required]
    public ClaimOrganisationDetails Organisation { get; set; }
}
