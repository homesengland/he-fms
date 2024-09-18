using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Reclaims;
public sealed class ReclaimPaymentRequest
{
    [Required]
    public ReclaimDetails Reclaim { get; set; }

    [Required]
    public ClaimApplicationDetails Application { get; set; }

    [Required]
    public ClaimOrganisationDetails Organisation { get; set; }

    public int EfinPartnerType
    {
        get
        {
            if (Reclaim.Amount != 0)
            {
                return EfinConstants.Default.Reclaim.AmountReclaim.Lookup.GetValueOrDefault(Organisation.PartnerType.RemoveSpecialCharacters());
            }

            if (Reclaim.InterestAmount != 0)
            {
                return EfinConstants.Default.Reclaim.InterestAmount.Lookup.GetValueOrDefault(Organisation.PartnerType.RemoveSpecialCharacters());
            }

            return default;
        }
    }
}
