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
                return Organisation.PartnerType.RemoveSpecialCharacters() switch
                {
                    nameof(EfinConstants.CapitalClaimPartnerType.ALBArmsLengthBodyofGovernment) => EfinConstants.CapitalClaimPartnerType.ALBArmsLengthBodyofGovernment,
                    nameof(EfinConstants.CapitalClaimPartnerType.ALMOArmsLengthManagementOrganisation) => EfinConstants.CapitalClaimPartnerType
                        .ALMOArmsLengthManagementOrganisation,
                    nameof(EfinConstants.CapitalClaimPartnerType.Bank) => EfinConstants.CapitalClaimPartnerType.Bank,
                    nameof(EfinConstants.CapitalClaimPartnerType.CombinedAuthority) => EfinConstants.CapitalClaimPartnerType.CombinedAuthority,
                    nameof(EfinConstants.CapitalClaimPartnerType.Consultant) => EfinConstants.CapitalClaimPartnerType.Consultant,
                    nameof(EfinConstants.CapitalClaimPartnerType.Education) => EfinConstants.CapitalClaimPartnerType.Education,
                    nameof(EfinConstants.CapitalClaimPartnerType.FinancialInstitution) => EfinConstants.CapitalClaimPartnerType.FinancialInstitution,
                    nameof(EfinConstants.CapitalClaimPartnerType.ForProfitRegisteredProvder) => EfinConstants.CapitalClaimPartnerType.ForProfitRegisteredProvder,
                    nameof(EfinConstants.CapitalClaimPartnerType.GovernmentPolicyMaker) => EfinConstants.CapitalClaimPartnerType.GovernmentPolicyMaker,
                    nameof(EfinConstants.CapitalClaimPartnerType.HealthandSocialCare) => EfinConstants.CapitalClaimPartnerType.HealthandSocialCare,
                    nameof(EfinConstants.CapitalClaimPartnerType.Insurer) => EfinConstants.CapitalClaimPartnerType.Insurer,
                    nameof(EfinConstants.CapitalClaimPartnerType.InvestmentManager) => EfinConstants.CapitalClaimPartnerType.InvestmentManager,
                    nameof(EfinConstants.CapitalClaimPartnerType.Landowner) => EfinConstants.CapitalClaimPartnerType.Landowner,
                    nameof(EfinConstants.CapitalClaimPartnerType.LocalAuthority) => EfinConstants.CapitalClaimPartnerType.LocalAuthority,
                    nameof(EfinConstants.CapitalClaimPartnerType.NonregisteredCharitableOrganisation) => EfinConstants.CapitalClaimPartnerType
                        .NonregisteredCharitableOrganisation,
                    nameof(EfinConstants.CapitalClaimPartnerType.NonBankLender) => EfinConstants.CapitalClaimPartnerType.NonBankLender,
                    nameof(EfinConstants.CapitalClaimPartnerType.NotForProfitRegisteredProvider) => EfinConstants.CapitalClaimPartnerType.NotForProfitRegisteredProvider,
                    nameof(EfinConstants.CapitalClaimPartnerType.Other) => EfinConstants.CapitalClaimPartnerType.Other,
                    nameof(EfinConstants.CapitalClaimPartnerType.OtherFinancialInstitutionorAdvisory) => EfinConstants.CapitalClaimPartnerType
                        .OtherFinancialInstitutionorAdvisory,
                    nameof(EfinConstants.CapitalClaimPartnerType.PrivateContractor) => EfinConstants.CapitalClaimPartnerType.PrivateContractor,
                    nameof(EfinConstants.CapitalClaimPartnerType.PrivateSectorHousebuilder) => EfinConstants.CapitalClaimPartnerType.PrivateSectorHousebuilder,
                    nameof(EfinConstants.CapitalClaimPartnerType.PublicPrivatePartnership) => EfinConstants.CapitalClaimPartnerType.PublicPrivatePartnership,
                    nameof(EfinConstants.CapitalClaimPartnerType.RegisteredCharitableOrganisation) => EfinConstants.CapitalClaimPartnerType
                        .RegisteredCharitableOrganisation,
                    nameof(EfinConstants.CapitalClaimPartnerType.TradeAssociation) => EfinConstants.CapitalClaimPartnerType.TradeAssociation,
                    nameof(EfinConstants.CapitalClaimPartnerType.UnregisteredHousingAssociation) => EfinConstants.CapitalClaimPartnerType.UnregisteredHousingAssociation,
                    _ => default,
                };
            }

            if (string.Equals(Application.RevenueIndicator, EfinConstants.RevenueIndicator.Revenue, StringComparison.OrdinalIgnoreCase))
            {
                return Organisation.PartnerType.RemoveSpecialCharacters() switch
                {
                    nameof(EfinConstants.RevenueClaimPartnerType.ALBArmsLengthBodyofGovernment) => EfinConstants.RevenueClaimPartnerType.ALBArmsLengthBodyofGovernment,
                    nameof(EfinConstants.RevenueClaimPartnerType.ALMOArmsLengthManagementOrganisation) => EfinConstants.RevenueClaimPartnerType
                        .ALMOArmsLengthManagementOrganisation,
                    nameof(EfinConstants.RevenueClaimPartnerType.Bank) => EfinConstants.RevenueClaimPartnerType.Bank,
                    nameof(EfinConstants.RevenueClaimPartnerType.CombinedAuthority) => EfinConstants.RevenueClaimPartnerType.CombinedAuthority,
                    nameof(EfinConstants.RevenueClaimPartnerType.Consultant) => EfinConstants.RevenueClaimPartnerType.Consultant,
                    nameof(EfinConstants.RevenueClaimPartnerType.Education) => EfinConstants.RevenueClaimPartnerType.Education,
                    nameof(EfinConstants.RevenueClaimPartnerType.FinancialInstitution) => EfinConstants.RevenueClaimPartnerType.FinancialInstitution,
                    nameof(EfinConstants.RevenueClaimPartnerType.ForProfitRegisteredProvder) => EfinConstants.RevenueClaimPartnerType.ForProfitRegisteredProvder,
                    nameof(EfinConstants.RevenueClaimPartnerType.GovernmentPolicyMaker) => EfinConstants.RevenueClaimPartnerType.GovernmentPolicyMaker,
                    nameof(EfinConstants.RevenueClaimPartnerType.HealthandSocialCare) => EfinConstants.RevenueClaimPartnerType.HealthandSocialCare,
                    nameof(EfinConstants.RevenueClaimPartnerType.Insurer) => EfinConstants.RevenueClaimPartnerType.Insurer,
                    nameof(EfinConstants.RevenueClaimPartnerType.InvestmentManager) => EfinConstants.RevenueClaimPartnerType.InvestmentManager,
                    nameof(EfinConstants.RevenueClaimPartnerType.Landowner) => EfinConstants.RevenueClaimPartnerType.Landowner,
                    nameof(EfinConstants.RevenueClaimPartnerType.LocalAuthority) => EfinConstants.RevenueClaimPartnerType.LocalAuthority,
                    nameof(EfinConstants.RevenueClaimPartnerType.NonregisteredCharitableOrganisation) => EfinConstants.RevenueClaimPartnerType
                        .NonregisteredCharitableOrganisation,
                    nameof(EfinConstants.RevenueClaimPartnerType.NonBankLender) => EfinConstants.RevenueClaimPartnerType.NonBankLender,
                    nameof(EfinConstants.RevenueClaimPartnerType.NotForProfitRegisteredProvider) => EfinConstants.RevenueClaimPartnerType.NotForProfitRegisteredProvider,
                    nameof(EfinConstants.RevenueClaimPartnerType.Other) => EfinConstants.RevenueClaimPartnerType.Other,
                    nameof(EfinConstants.RevenueClaimPartnerType.OtherFinancialInstitutionorAdvisory) => EfinConstants.RevenueClaimPartnerType
                        .OtherFinancialInstitutionorAdvisory,
                    nameof(EfinConstants.RevenueClaimPartnerType.PrivateContractor) => EfinConstants.RevenueClaimPartnerType.PrivateContractor,
                    nameof(EfinConstants.RevenueClaimPartnerType.PrivateSectorHousebuilder) => EfinConstants.RevenueClaimPartnerType.PrivateSectorHousebuilder,
                    nameof(EfinConstants.RevenueClaimPartnerType.PublicPrivatePartnership) => EfinConstants.RevenueClaimPartnerType.PublicPrivatePartnership,
                    nameof(EfinConstants.RevenueClaimPartnerType.RegisteredCharitableOrganisation) => EfinConstants.RevenueClaimPartnerType
                        .RegisteredCharitableOrganisation,
                    nameof(EfinConstants.RevenueClaimPartnerType.TradeAssociation) => EfinConstants.RevenueClaimPartnerType.TradeAssociation,
                    nameof(EfinConstants.RevenueClaimPartnerType.UnregisteredHousingAssociation) => EfinConstants.RevenueClaimPartnerType.UnregisteredHousingAssociation,
                    _ => default,
                };
            }

            return default;
        }
    }
}
