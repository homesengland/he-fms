using System.Globalization;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Reclaims.Efin;
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
public class CLI_IW_INV
{
    [EfinFileRowIndex(1, 3)]
    public string cliwi_sub_ledger_id { get; set; }

    [EfinFileRowIndex(4, 11)]
    public string cliwi_inv_ref { get; set; }

    [EfinFileRowIndex(12, 18)]
    public string cliwi_batch_ref { get; set; }

    [EfinFileRowIndex(19, 26)]
    public string cliwi_invoice_to_id { get; set; }

    [EfinFileRowIndex(266, 281)]
    public string cliwi_net_amount { get; set; }

    [EfinFileRowIndex(301, 315)]
    public string cliwi_their_ref { get; set; }

    [EfinFileRowIndex(316, 316)]
    public string cliwi_trans_type { get; set; }

    [EfinFileRowIndex(319, 327)]
    public string cliwi_date { get; set; }

    [EfinFileRowIndex(328, 329)]
    public string cliwi_terms_code { get; set; }

    [EfinFileRowIndex(339, 344)]
    public string cliwi_cost_centre { get; set; }

    [EfinFileRowIndex(345, 352)]
    public string cliwi_job { get; set; }

    [EfinFileRowIndex(353, 360)]
    public string cliwi_account { get; set; }

    [EfinFileRowIndex(361, 366)]
    public string cliwi_activity { get; set; }

    [EfinFileRowIndex(367, 375)]
    public string cliwi_entry_date { get; set; }

    [EfinFileRowIndex(376, 376)]
    public string cliwi_invoice_prefix { get; set; }

    [EfinFileRowIndex(377, 385)]
    public string cliwi_tax_point { get; set; }

    [EfinFileRowIndex(544, 583)]
    public string cliwi_description { get; set; }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
