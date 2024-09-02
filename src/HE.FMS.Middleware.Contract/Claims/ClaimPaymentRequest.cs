using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Claims;
public sealed class ClaimPaymentRequest
{
    [Required]
    public ClaimDetails Claim { get; set; }

    [Required]
    public ClaimApplicationDetails Application { get; set; }

    [Required]
    public ClaimOrganisationDetails Organisation { get; set; }

    public string EfinSchemeName => Application.Name.Truncate(24) + Claim.EfinMilestoneName;

    public int EfiPartnerType
    {
        get
        {
            if (string.Equals(Application.RevenueIndicator, EfinConstants.RevenueIndicator.Capital, StringComparison.OrdinalIgnoreCase))
            {
                return Organisation.PartnerType.RemoveSpecialCharacters() switch
                {
                    nameof(EfinConstants.CapitalPartnerType.ALBArmsLengthBodyofGovernment) => EfinConstants.CapitalPartnerType.ALBArmsLengthBodyofGovernment,
                    nameof(EfinConstants.CapitalPartnerType.ALMOArmsLengthManagementOrganisation) => EfinConstants.CapitalPartnerType
                        .ALMOArmsLengthManagementOrganisation,
                    nameof(EfinConstants.CapitalPartnerType.Bank) => EfinConstants.CapitalPartnerType.Bank,
                    nameof(EfinConstants.CapitalPartnerType.CombinedAuthority) => EfinConstants.CapitalPartnerType.CombinedAuthority,
                    nameof(EfinConstants.CapitalPartnerType.Consultant) => EfinConstants.CapitalPartnerType.Consultant,
                    nameof(EfinConstants.CapitalPartnerType.Education) => EfinConstants.CapitalPartnerType.Education,
                    nameof(EfinConstants.CapitalPartnerType.FinancialInstitution) => EfinConstants.CapitalPartnerType.FinancialInstitution,
                    nameof(EfinConstants.CapitalPartnerType.ForProfitRegisteredProvder) => EfinConstants.CapitalPartnerType.ForProfitRegisteredProvder,
                    nameof(EfinConstants.CapitalPartnerType.GovernmentPolicyMaker) => EfinConstants.CapitalPartnerType.GovernmentPolicyMaker,
                    nameof(EfinConstants.CapitalPartnerType.HealthandSocialCare) => EfinConstants.CapitalPartnerType.HealthandSocialCare,
                    nameof(EfinConstants.CapitalPartnerType.Insurer) => EfinConstants.CapitalPartnerType.Insurer,
                    nameof(EfinConstants.CapitalPartnerType.InvestmentManager) => EfinConstants.CapitalPartnerType.InvestmentManager,
                    nameof(EfinConstants.CapitalPartnerType.Landowner) => EfinConstants.CapitalPartnerType.Landowner,
                    nameof(EfinConstants.CapitalPartnerType.LocalAuthority) => EfinConstants.CapitalPartnerType.LocalAuthority,
                    nameof(EfinConstants.CapitalPartnerType.NonregisteredCharitableOrganisation) => EfinConstants.CapitalPartnerType
                        .NonregisteredCharitableOrganisation,
                    nameof(EfinConstants.CapitalPartnerType.NonBankLender) => EfinConstants.CapitalPartnerType.NonBankLender,
                    nameof(EfinConstants.CapitalPartnerType.NotForProfitRegisteredProvider) => EfinConstants.CapitalPartnerType.NotForProfitRegisteredProvider,
                    nameof(EfinConstants.CapitalPartnerType.Other) => EfinConstants.CapitalPartnerType.Other,
                    nameof(EfinConstants.CapitalPartnerType.OtherFinancialInstitutionorAdvisory) => EfinConstants.CapitalPartnerType
                        .OtherFinancialInstitutionorAdvisory,
                    nameof(EfinConstants.CapitalPartnerType.PrivateContractor) => EfinConstants.CapitalPartnerType.PrivateContractor,
                    nameof(EfinConstants.CapitalPartnerType.PrivateSectorHousebuilder) => EfinConstants.CapitalPartnerType.PrivateSectorHousebuilder,
                    nameof(EfinConstants.CapitalPartnerType.PublicPrivatePartnership) => EfinConstants.CapitalPartnerType.PublicPrivatePartnership,
                    nameof(EfinConstants.CapitalPartnerType.RegisteredCharitableOrganisation) => EfinConstants.CapitalPartnerType
                        .RegisteredCharitableOrganisation,
                    nameof(EfinConstants.CapitalPartnerType.TradeAssociation) => EfinConstants.CapitalPartnerType.TradeAssociation,
                    nameof(EfinConstants.CapitalPartnerType.UnregisteredHousingAssociation) => EfinConstants.CapitalPartnerType.UnregisteredHousingAssociation,
                    _ => default,
                };
            }

            if (string.Equals(Application.RevenueIndicator, EfinConstants.RevenueIndicator.Revenue, StringComparison.OrdinalIgnoreCase))
            {
                return Organisation.PartnerType.RemoveSpecialCharacters() switch
                {
                    nameof(EfinConstants.RevenuePartnerType.ALBArmsLengthBodyofGovernment) => EfinConstants.RevenuePartnerType.ALBArmsLengthBodyofGovernment,
                    nameof(EfinConstants.RevenuePartnerType.ALMOArmsLengthManagementOrganisation) => EfinConstants.RevenuePartnerType
                        .ALMOArmsLengthManagementOrganisation,
                    nameof(EfinConstants.RevenuePartnerType.Bank) => EfinConstants.RevenuePartnerType.Bank,
                    nameof(EfinConstants.RevenuePartnerType.CombinedAuthority) => EfinConstants.RevenuePartnerType.CombinedAuthority,
                    nameof(EfinConstants.RevenuePartnerType.Consultant) => EfinConstants.RevenuePartnerType.Consultant,
                    nameof(EfinConstants.RevenuePartnerType.Education) => EfinConstants.RevenuePartnerType.Education,
                    nameof(EfinConstants.RevenuePartnerType.FinancialInstitution) => EfinConstants.RevenuePartnerType.FinancialInstitution,
                    nameof(EfinConstants.RevenuePartnerType.ForProfitRegisteredProvder) => EfinConstants.RevenuePartnerType.ForProfitRegisteredProvder,
                    nameof(EfinConstants.RevenuePartnerType.GovernmentPolicyMaker) => EfinConstants.RevenuePartnerType.GovernmentPolicyMaker,
                    nameof(EfinConstants.RevenuePartnerType.HealthandSocialCare) => EfinConstants.RevenuePartnerType.HealthandSocialCare,
                    nameof(EfinConstants.RevenuePartnerType.Insurer) => EfinConstants.RevenuePartnerType.Insurer,
                    nameof(EfinConstants.RevenuePartnerType.InvestmentManager) => EfinConstants.RevenuePartnerType.InvestmentManager,
                    nameof(EfinConstants.RevenuePartnerType.Landowner) => EfinConstants.RevenuePartnerType.Landowner,
                    nameof(EfinConstants.RevenuePartnerType.LocalAuthority) => EfinConstants.RevenuePartnerType.LocalAuthority,
                    nameof(EfinConstants.RevenuePartnerType.NonregisteredCharitableOrganisation) => EfinConstants.RevenuePartnerType
                        .NonregisteredCharitableOrganisation,
                    nameof(EfinConstants.RevenuePartnerType.NonBankLender) => EfinConstants.RevenuePartnerType.NonBankLender,
                    nameof(EfinConstants.RevenuePartnerType.NotForProfitRegisteredProvider) => EfinConstants.RevenuePartnerType.NotForProfitRegisteredProvider,
                    nameof(EfinConstants.RevenuePartnerType.Other) => EfinConstants.RevenuePartnerType.Other,
                    nameof(EfinConstants.RevenuePartnerType.OtherFinancialInstitutionorAdvisory) => EfinConstants.RevenuePartnerType
                        .OtherFinancialInstitutionorAdvisory,
                    nameof(EfinConstants.RevenuePartnerType.PrivateContractor) => EfinConstants.RevenuePartnerType.PrivateContractor,
                    nameof(EfinConstants.RevenuePartnerType.PrivateSectorHousebuilder) => EfinConstants.RevenuePartnerType.PrivateSectorHousebuilder,
                    nameof(EfinConstants.RevenuePartnerType.PublicPrivatePartnership) => EfinConstants.RevenuePartnerType.PublicPrivatePartnership,
                    nameof(EfinConstants.RevenuePartnerType.RegisteredCharitableOrganisation) => EfinConstants.RevenuePartnerType
                        .RegisteredCharitableOrganisation,
                    nameof(EfinConstants.RevenuePartnerType.TradeAssociation) => EfinConstants.RevenuePartnerType.TradeAssociation,
                    nameof(EfinConstants.RevenuePartnerType.UnregisteredHousingAssociation) => EfinConstants.RevenuePartnerType.UnregisteredHousingAssociation,
                    _ => default,
                };
            }

            return default;
        }
    }
}
