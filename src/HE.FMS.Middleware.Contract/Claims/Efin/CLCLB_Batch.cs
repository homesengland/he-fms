using System.Globalization;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Constants;

#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
namespace HE.FMS.Middleware.Contract.Claims.Efin;
public class CLCLB_Batch
{
    [EfinFileRowIndex(1, 3)]
    public string clb_sub_ledger { get; set; }

    [EfinFileRowIndex(5, 11)]
    public string clb_batch_ref { get; set; }

    [EfinFileRowIndex(13, 16)]
    public string clb_year { get; set; }

    [EfinFileRowIndex(18, 19)]
    public string clb_period { get; set; }

    [EfinFileRowIndex(23, 39)]
    public string clb_net_amount { get; set; }

    [EfinFileRowIndex(43, 59)]
    public string clb_vat_amount { get; set; }

    [EfinFileRowIndex(61, 67)]
    public string clb_no_invoices { get; set; }

    [EfinFileRowIndex(77, 96)]
    public string clb_user { get; set; }

    [EfinFileRowIndex(116, 132)]
    public string clb_quantity { get; set; }

    [EfinFileRowIndex(156, 156)]
    public string clb_grouping { get; set; }

    [EfinFileRowIndex(192, 200)]
    public string clb_entry_date { get; set; }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
