using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Claims;
public sealed class ClaimPaymentRequest
{
    [Required]
    public ClaimDetailsBase Claim { get; set; }

    [Required]
    public ClaimApplicationDetails Application { get; set; }

    [Required]
    public ClaimOrganisationDetails Organisation { get; set; }

    public string EfinSchemeName => Application.Name.Truncate(24) + Claim.EfinMilestoneName;

    public int EfinPartnerType
    {
        get
        {
            if (string.Equals(Application.RevenueIndicator, EfinConstants.RevenueIndicator.Capital, StringComparison.OrdinalIgnoreCase))
            {
                return EfinConstants.Default.Claim.CapitalPartnerType.Lookup.GetValueOrDefault(Organisation.PartnerType.RemoveSpecialCharacters());
            }

            if (string.Equals(Application.RevenueIndicator, EfinConstants.RevenueIndicator.Revenue, StringComparison.OrdinalIgnoreCase))
            {
                return EfinConstants.Default.Claim.RevenuePartnerType.Lookup.GetValueOrDefault(Organisation.PartnerType.RemoveSpecialCharacters());
            }

            return default;
        }
    }
}
