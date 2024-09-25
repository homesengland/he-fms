using HE.FMS.Middleware.BusinessLogic.Constants;
using HE.FMS.Middleware.BusinessLogic.Efin;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
public sealed class FakeEfinLookupService : IEfinLookupCacheService
{
    public Task<Dictionary<string, string>> GetValue(string key)
    {
        var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (key.Equals(EfinConstants.Lookups.ClaimDefault, StringComparison.OrdinalIgnoreCase))
        {
            dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "cla_sub_ledger", "PL4" },
                { "cla_uom", "EA" },
                { "cla_unit_qty", "1" },
                { "cla_volume", "1" },
                { "clb_grouping", "C" },
                { "clb_sub_ledger", "PL4" },
                { "clb_user", "AH GRANTS" },
                { "clb_vat_amount", "0" },
                { "cli_sub_ledger", "PL4" },
                { "cli_terms_code", "00" },
                { "cli_trans_type", "PI" },
                { "cli_uom", "EA" },
                { "cli_vat", "0" },
                { "cli_volume", "1" },
                { "cli_job", "X0000089" },
                { "cla_cfacs_job", "X0000089" },
            };
        }
        else if (key.Equals(EfinConstants.Lookups.ReclaimDefault, StringComparison.OrdinalIgnoreCase))
        {
            dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "cliwa_item_sequence", "1" },
                { "cliwa_pre_pay_yn", "N" },
                { "cliwa_sub_ledger_id", "SL4" },
                { "cliwa_uom", "EA" },
                { "cliwb_default_prefix", "H" },
                { "cliwb_description", "AHP-RECLAIM" },
                { "cliwb_sub_ledger", "SL4" },
                { "cliwb_user", "GRANTS" },
                { "cliwl_item_sequence", "1" },
                { "cliwl_line_no", "1" },
                { "cliwl_line_ref", "1" },
                { "cliwl_product_id", "GRANT RECLAIM" },
                { "cliwl_sub_ledger_id", "SL4" },
                { "cliwx_header_footer", "H" },
                { "cliwx_line_no", "1" },
                { "cliwx_sub_ledger_id", "SL4" },
                { "cliwi_invoice_prefix", "H" },
                { "cliwi_sub_ledger_id", "SL4" },
                { "cliwi_terms_code", "00" },
                { "cliwi_trans_type", "I" },
                { "cliwt_item_sequence", "1" },
                { "cliwt_print_sequence", "1" },
                { "cliwt_sub_ledger_id", "SL4" },
                { "cliwt_text", "HOUSING FOR RENT GRANT RECLAIM" },
                { "cliwa_job", "X0000089" },
                { "cliwi_job", "X0000089" },
            };
        }
        else if (key.Equals(EfinConstants.Lookups.RegionLookup, StringComparison.OrdinalIgnoreCase))
        {
            dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "north", "ASWRN" },
                { "south", "ASWRS" },
                { "midlands", "ASWRM" },
                { "london", "ASWRL" },
            };
        }
        else if (key.Equals(EfinConstants.Lookups.MilestoneLookup, StringComparison.OrdinalIgnoreCase))
        {
            dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Acquisition", "ACQ" },
                { "StartOnSite", "SOS" },
                { "PracticalCompletion", "PC" },
                { "Planning", "PLA" },
            };
        }
        else if (key.Equals(EfinConstants.Lookups.TenureLookup, StringComparison.OrdinalIgnoreCase))
        {
            dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "AffordableRent", "7572" },
                { "SocialRent", "7667" },
                { "SharedOwnership", "7666" },
                { "RentToBuy", "7655" },
                { "HomeOwnershipForPeopleWithLongTermDisabilities", "7666" },
                { "OlderPersonsSharedOwnership", "7666" },
            };
        }
        else if (key.Equals(EfinConstants.Lookups.RevenueIndicatorLookup, StringComparison.OrdinalIgnoreCase))
        {
            dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Revenue", "Revenue" },
                { "Capital", "Capital" },
            };
        }
        else if (key.Equals(EfinConstants.Lookups.PartnerTypeLookup, StringComparison.OrdinalIgnoreCase))
        {
            dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Claim_Capital_ArmsLengthBodyofGovernment", "1552" },
                { "Claim_Capital_ArmsLengthManagementOrganisation", "1553" },
                { "Claim_Capital_Bank", "1551" },
                { "Claim_Capital_CombinedAuthority", "1553" },
                { "Claim_Capital_Consultant", "1551" },
                { "Claim_Capital_Education", "1552" },
                { "Claim_Capital_FinancialInstitution", "1551" },
                { "Claim_Capital_ForProfitRegisteredProvder", "1558" },
                { "Claim_Capital_GovernmentPolicyMaker", "1552" },
                { "Claim_Capital_HealthandSocialCare", "1552" },
                { "Claim_Capital_Insurer", "1551" },
                { "Claim_Capital_InvestmentManager", "1551" },
                { "Claim_Capital_Landowner", "1551" },
                { "Claim_Capital_LocalAuthority", "1553" },
                { "Claim_Capital_NonRegisteredCharitableOrganisation", "1551" },
                { "Claim_Capital_NonBankLender", "1558" },
                { "Claim_Capital_NotForProfitRegisteredProvider", "1558" },
                { "Claim_Capital_Other", "1551" },
                { "Claim_Capital_OtherFinancialInstitutionorAdvisory", "1551" },
                { "Claim_Capital_PrivateContractor", "1551" },
                { "Claim_Capital_PrivateSectorHousebuilder", "1551" },
                { "Claim_Capital_PublicPrivatePartnership", "1552" },
                { "Claim_Capital_RegisteredCharitableOrganisation", "1558" },
                { "Claim_Capital_TradeAssociation", "1551" },
                { "Claim_Capital_UnregisteredHousingAssociation", "1558" },
                { "Claim_Revenue_ArmsLengthBodyofGovernment", "1554" },
                { "Claim_Revenue_ArmsLengthManagementOrganisation", "1555" },
                { "Claim_Revenue_Bank", "1575" },
                { "Claim_Revenue_CombinedAuthority", "1555" },
                { "Claim_Revenue_Consultant", "1575" },
                { "Claim_Revenue_Education", "1555" },
                { "Claim_Revenue_FinancialInstitution", "1575" },
                { "Claim_Revenue_ForProfitRegisteredProvder", "1559" },
                { "Claim_Revenue_GovernmentPolicyMaker", "1554" },
                { "Claim_Revenue_HealthandSocialCare", "1555" },
                { "Claim_Revenue_Insurer", "1575" },
                { "Claim_Revenue_InvestmentManager", "1575" },
                { "Claim_Revenue_Landowner", "1575" },
                { "Claim_Revenue_LocalAuthority", "1555" },
                { "Claim_Revenue_NonregisteredCharitableOrganisation", "1575" },
                { "Claim_Revenue_NonBankLender", "1559" },
                { "Claim_Revenue_NotForProfitRegisteredProvider", "1559" },
                { "Claim_Revenue_Other", "1575" },
                { "Claim_Revenue_OtherFinancialInstitutionorAdvisory", "1575" },
                { "Claim_Revenue_PrivateContractor", "1575" },
                { "Claim_Revenue_PrivateSectorHousebuilder", "1575" },
                { "Claim_Revenue_PublicPrivatePartnership", "1554" },
                { "Claim_Revenue_RegisteredCharitableOrganisation", "1559" },
                { "Claim_Revenue_TradeAssociation", "1575" },
                { "Claim_Revenue_UnregisteredHousingAssociation", "1559" },
                { "Reclaim_Amount_ArmsLengthBodyofGovernment", "0028" },
                { "Reclaim_Amount_ArmsLengthManagementOrganisation", "0027" },
                { "Reclaim_Amount_Bank", "0029" },
                { "Reclaim_Amount_CombinedAuthority", "0027" },
                { "Reclaim_Amount_Consultant", "0029" },
                { "Reclaim_Amount_Education", "0028" },
                { "Reclaim_Amount_FinancialInstitution", "0029" },
                { "Reclaim_Amount_ForProfitRegisteredProvder", "0030" },
                { "Reclaim_Amount_GovernmentPolicyMaker", "0028" },
                { "Reclaim_Amount_HealthandSocialCare", "0028" },
                { "Reclaim_Amount_Insurer", "0029" },
                { "Reclaim_Amount_InvestmentManager", "0029" },
                { "Reclaim_Amount_Landowner", "0029" },
                { "Reclaim_Amount_LocalAuthority", "0027" },
                { "Reclaim_Amount_NonregisteredCharitableOrganisation", "0029" },
                { "Reclaim_Amount_NonBankLender", "0030" },
                { "Reclaim_Amount_NotForProfitRegisteredProvider", "0030" },
                { "Reclaim_Amount_Other", "0029" },
                { "Reclaim_Amount_OtherFinancialInstitutionorAdvisory", "0029" },
                { "Reclaim_Amount_PrivateContractor", "0029" },
                { "Reclaim_Amount_PrivateSectorHousebuilder", "0029" },
                { "Reclaim_Amount_PublicPrivatePartnership", "0028" },
                { "Reclaim_Amount_RegisteredCharitableOrganisation", "0030" },
                { "Reclaim_Amount_TradeAssociation", "0029" },
                { "Reclaim_Amount_UnregisteredHousingAssociation", "0030" },
                { "Reclaim_InterestAmount_ArmsLengthBodyofGovernment", "0067" },
                { "Reclaim_InterestAmount_ArmsLengthManagementOrganisation", "0067" },
                { "Reclaim_InterestAmount_Bank", "0067" },
                { "Reclaim_InterestAmount_CombinedAuthority", "0067" },
                { "Reclaim_InterestAmount_Consultant", "0067" },
                { "Reclaim_InterestAmount_Education", "0067" },
                { "Reclaim_InterestAmount_FinancialInstitution", "0067" },
                { "Reclaim_InterestAmount_ForProfitRegisteredProvder", "0067" },
                { "Reclaim_InterestAmount_GovernmentPolicyMaker", "0067" },
                { "Reclaim_InterestAmount_HealthandSocialCare", "0067" },
                { "Reclaim_InterestAmount_Insurer", "0067" },
                { "Reclaim_InterestAmount_InvestmentManager", "0067" },
                { "Reclaim_InterestAmount_Landowner", "0067" },
                { "Reclaim_InterestAmount_LocalAuthority", "0067" },
                { "Reclaim_InterestAmount_NonregisteredCharitableOrganisation", "0067" },
                { "Reclaim_InterestAmount_NonBankLender", "0067" },
                { "Reclaim_InterestAmount_NotForProfitRegisteredProvider", "0067" },
                { "Reclaim_InterestAmount_Other", "0067" },
                { "Reclaim_InterestAmount_OtherFinancialInstitutionorAdvisory", "0067" },
                { "Reclaim_InterestAmount_PrivateContractor", "0067" },
                { "Reclaim_InterestAmount_PrivateSectorHousebuilder", "0067" },
                { "Reclaim_InterestAmount_PublicPrivatePartnership", "0067" },
                { "Reclaim_InterestAmount_RegisteredCharitableOrganisation", "0067" },
                { "Reclaim_InterestAmount_TradeAssociation", "0067" },
                { "Reclaim_InterestAmount_UnregisteredHousingAssociation", "0067" },
            };
        }

        return Task.FromResult(dictionary);
    }

    public void InvalidateKey(string key)
    {
        throw new NotImplementedException();
    }
}
