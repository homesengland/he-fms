using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
public sealed class FakeEfinLookupService : IEfinLookupCacheService
{
    public Task<Dictionary<string, string>> GetValue(string key)
    {
        var dict = new Dictionary<string, string>();

        if (key.Equals(EfinConstants.Lookups.ClaimDefault, StringComparison.OrdinalIgnoreCase))
        {
            dict = new Dictionary<string, string>
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
            };
        }
        else if (key.Equals(EfinConstants.Lookups.ReclaimDefault, StringComparison.OrdinalIgnoreCase))
        {
            dict = new Dictionary<string, string>
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
            };
        }
        else if (key.Equals(EfinConstants.Lookups.RegionLookup, StringComparison.OrdinalIgnoreCase))
        {
            dict = new Dictionary<string, string>
            {
                { "North", "ASWRN" },
                { "South", "ASWRS" },
                { "Midlands", "ASWRM" },
                { "London", "ASWRL" },
            };
        }
        else if (key.Equals(EfinConstants.Lookups.MilestoneLookup, StringComparison.OrdinalIgnoreCase))
        {
            dict = new Dictionary<string, string>
            {
                { "Acquisition", "ACQ" },
                { "StartOnSite", "SOS" },
                { "PracticalCompletion", "PC" },
            };
        }
        else if (key.Equals(EfinConstants.Lookups.TenureLookup, StringComparison.OrdinalIgnoreCase))
        {
            dict = new Dictionary<string, string>
            {
                { "AffordableRent", "7572" },
                { "SocialRent", "7667" },
                { "SharedOwnership", "7666" },
                { "RentToBuy", "7655" },
                { "HOLD", "7666" },
                { "OPSO", "7666" },
            };
        }
        else if (key.Equals(EfinConstants.Lookups.RevenueIndicatorLookup, StringComparison.OrdinalIgnoreCase))
        {
            dict = new Dictionary<string, string>
            {
                { "Revenue", "Revenue" },
                { "Capital", "Capital" },
            };
        }

        return Task.FromResult(dict);
    }

    public void InvalidateKey(string key)
    {
        throw new NotImplementedException();
    }
}
