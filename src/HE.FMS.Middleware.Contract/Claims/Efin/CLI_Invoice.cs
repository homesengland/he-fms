using System.Globalization;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Constants;

#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
namespace HE.FMS.Middleware.Contract.Claims.Efin;
public class CLI_Invoice
{
    [EfinFileRowIndex(1, 3)]
    public string cli_sub_ledger { get; set; }

    [EfinFileRowIndex(5, 12)]
    public string cli_inv_ref { get; set; }

    [EfinFileRowIndex(14, 20)]
    public string cli_batch_ref { get; set; }

    [EfinFileRowIndex(22, 29)]
    public string cli_cfacs_customer { get; set; }

    [EfinFileRowIndex(39, 55)]
    public string cli_net_amount { get; set; }

    [EfinFileRowIndex(59, 75)]
    public string cli_vat { get; set; }

    [EfinFileRowIndex(79, 95)]
    public string cli_gross { get; set; }

    [EfinFileRowIndex(121, 137)]
    public string cli_volume { get; set; }

    [EfinFileRowIndex(139, 141)]
    public string cli_uom { get; set; }

    [EfinFileRowIndex(143, 157)]
    public string cli_our_ref_2 { get; set; }

    [EfinFileRowIndex(159, 173)]
    public string cli_their_ref { get; set; }

    [EfinFileRowIndex(175, 176)]
    public string cli_trans_type { get; set; }

    [EfinFileRowIndex(181, 189)]
    public string cli_date { get; set; }

    [EfinFileRowIndex(232, 261)]
    public string cli_description { get; set; }

    [EfinFileRowIndex(263, 264)]
    public string cli_terms_code { get; set; }

    [EfinFileRowIndex(266, 274)]
    public string cli_due_date { get; set; }

    [EfinFileRowIndex(276, 281)]
    public string cli_cost_centre { get; set; }

    [EfinFileRowIndex(283, 290)]
    public string cli_job { get; set; }

    [EfinFileRowIndex(313, 318)]
    public string cli_activity { get; set; }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
