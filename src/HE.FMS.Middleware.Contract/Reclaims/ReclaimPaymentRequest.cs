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

    public string EfinSchemeName => Application.Name.Truncate(24) + Reclaim.EfinMilestoneName;

    public int EfinPartnerType
    {
        get
        {
            if (Reclaim.Amount != 0 && Reclaim.InterestAmount != 0)
            {
                return default;
            }

            if (Reclaim.Amount != 0)
            {
                return Organisation.PartnerType.RemoveSpecialCharacters() switch
                {
                    nameof(EfinConstants.AmountReclaim.ALBArmsLengthBodyofGovernment) => EfinConstants.AmountReclaim.ALBArmsLengthBodyofGovernment,
                    nameof(EfinConstants.AmountReclaim.ALMOArmsLengthManagementOrganisation) => EfinConstants.AmountReclaim
                        .ALMOArmsLengthManagementOrganisation,
                    nameof(EfinConstants.AmountReclaim.Bank) => EfinConstants.AmountReclaim.Bank,
                    nameof(EfinConstants.AmountReclaim.CombinedAuthority) => EfinConstants.AmountReclaim.CombinedAuthority,
                    nameof(EfinConstants.AmountReclaim.Consultant) => EfinConstants.AmountReclaim.Consultant,
                    nameof(EfinConstants.AmountReclaim.Education) => EfinConstants.AmountReclaim.Education,
                    nameof(EfinConstants.AmountReclaim.FinancialInstitution) => EfinConstants.AmountReclaim.FinancialInstitution,
                    nameof(EfinConstants.AmountReclaim.ForProfitRegisteredProvder) => EfinConstants.AmountReclaim.ForProfitRegisteredProvder,
                    nameof(EfinConstants.AmountReclaim.GovernmentPolicyMaker) => EfinConstants.AmountReclaim.GovernmentPolicyMaker,
                    nameof(EfinConstants.AmountReclaim.HealthandSocialCare) => EfinConstants.AmountReclaim.HealthandSocialCare,
                    nameof(EfinConstants.AmountReclaim.Insurer) => EfinConstants.AmountReclaim.Insurer,
                    nameof(EfinConstants.AmountReclaim.InvestmentManager) => EfinConstants.AmountReclaim.InvestmentManager,
                    nameof(EfinConstants.AmountReclaim.Landowner) => EfinConstants.AmountReclaim.Landowner,
                    nameof(EfinConstants.AmountReclaim.LocalAuthority) => EfinConstants.AmountReclaim.LocalAuthority,
                    nameof(EfinConstants.AmountReclaim.NonregisteredCharitableOrganisation) => EfinConstants.AmountReclaim
                        .NonregisteredCharitableOrganisation,
                    nameof(EfinConstants.AmountReclaim.NonBankLender) => EfinConstants.AmountReclaim.NonBankLender,
                    nameof(EfinConstants.AmountReclaim.NotForProfitRegisteredProvider) => EfinConstants.AmountReclaim.NotForProfitRegisteredProvider,
                    nameof(EfinConstants.AmountReclaim.Other) => EfinConstants.AmountReclaim.Other,
                    nameof(EfinConstants.AmountReclaim.OtherFinancialInstitutionorAdvisory) => EfinConstants.AmountReclaim
                        .OtherFinancialInstitutionorAdvisory,
                    nameof(EfinConstants.AmountReclaim.PrivateContractor) => EfinConstants.AmountReclaim.PrivateContractor,
                    nameof(EfinConstants.AmountReclaim.PrivateSectorHousebuilder) => EfinConstants.AmountReclaim.PrivateSectorHousebuilder,
                    nameof(EfinConstants.AmountReclaim.PublicPrivatePartnership) => EfinConstants.AmountReclaim.PublicPrivatePartnership,
                    nameof(EfinConstants.AmountReclaim.RegisteredCharitableOrganisation) => EfinConstants.AmountReclaim
                        .RegisteredCharitableOrganisation,
                    nameof(EfinConstants.AmountReclaim.TradeAssociation) => EfinConstants.AmountReclaim.TradeAssociation,
                    nameof(EfinConstants.AmountReclaim.UnregisteredHousingAssociation) => EfinConstants.AmountReclaim.UnregisteredHousingAssociation,
                    _ => default,
                };
            }

            if (Reclaim.InterestAmount != 0)
            {
                return Organisation.PartnerType.RemoveSpecialCharacters() switch
                {
                    nameof(EfinConstants.InterestAmount.ALBArmsLengthBodyofGovernment) => EfinConstants.InterestAmount.ALBArmsLengthBodyofGovernment,
                    nameof(EfinConstants.InterestAmount.ALMOArmsLengthManagementOrganisation) => EfinConstants.InterestAmount
                        .ALMOArmsLengthManagementOrganisation,
                    nameof(EfinConstants.InterestAmount.Bank) => EfinConstants.InterestAmount.Bank,
                    nameof(EfinConstants.InterestAmount.CombinedAuthority) => EfinConstants.InterestAmount.CombinedAuthority,
                    nameof(EfinConstants.InterestAmount.Consultant) => EfinConstants.InterestAmount.Consultant,
                    nameof(EfinConstants.InterestAmount.Education) => EfinConstants.InterestAmount.Education,
                    nameof(EfinConstants.InterestAmount.FinancialInstitution) => EfinConstants.InterestAmount.FinancialInstitution,
                    nameof(EfinConstants.InterestAmount.ForProfitRegisteredProvder) => EfinConstants.InterestAmount.ForProfitRegisteredProvder,
                    nameof(EfinConstants.InterestAmount.GovernmentPolicyMaker) => EfinConstants.InterestAmount.GovernmentPolicyMaker,
                    nameof(EfinConstants.InterestAmount.HealthandSocialCare) => EfinConstants.InterestAmount.HealthandSocialCare,
                    nameof(EfinConstants.InterestAmount.Insurer) => EfinConstants.InterestAmount.Insurer,
                    nameof(EfinConstants.InterestAmount.InvestmentManager) => EfinConstants.InterestAmount.InvestmentManager,
                    nameof(EfinConstants.InterestAmount.Landowner) => EfinConstants.InterestAmount.Landowner,
                    nameof(EfinConstants.InterestAmount.LocalAuthority) => EfinConstants.InterestAmount.LocalAuthority,
                    nameof(EfinConstants.InterestAmount.NonregisteredCharitableOrganisation) => EfinConstants.InterestAmount
                        .NonregisteredCharitableOrganisation,
                    nameof(EfinConstants.InterestAmount.NonBankLender) => EfinConstants.InterestAmount.NonBankLender,
                    nameof(EfinConstants.InterestAmount.NotForProfitRegisteredProvider) => EfinConstants.InterestAmount.NotForProfitRegisteredProvider,
                    nameof(EfinConstants.InterestAmount.Other) => EfinConstants.InterestAmount.Other,
                    nameof(EfinConstants.InterestAmount.OtherFinancialInstitutionorAdvisory) => EfinConstants.InterestAmount
                        .OtherFinancialInstitutionorAdvisory,
                    nameof(EfinConstants.InterestAmount.PrivateContractor) => EfinConstants.InterestAmount.PrivateContractor,
                    nameof(EfinConstants.InterestAmount.PrivateSectorHousebuilder) => EfinConstants.InterestAmount.PrivateSectorHousebuilder,
                    nameof(EfinConstants.InterestAmount.PublicPrivatePartnership) => EfinConstants.InterestAmount.PublicPrivatePartnership,
                    nameof(EfinConstants.InterestAmount.RegisteredCharitableOrganisation) => EfinConstants.InterestAmount
                        .RegisteredCharitableOrganisation,
                    nameof(EfinConstants.InterestAmount.TradeAssociation) => EfinConstants.InterestAmount.TradeAssociation,
                    nameof(EfinConstants.InterestAmount.UnregisteredHousingAssociation) => EfinConstants.InterestAmount.UnregisteredHousingAssociation,
                    _ => default,
                };
            }

            return default;
        }
    }
}
