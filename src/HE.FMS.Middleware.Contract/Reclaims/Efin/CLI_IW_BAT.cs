using System.Globalization;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Reclaims.Efin;
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
public class CLI_IW_BAT
{
    [EfinFileRowIndex(1, 3)]
    public string cliwb_sub_ledger { get; set; }

    [EfinFileRowIndex(4, 10)]
    public string cliwb_batch_ref { get; set; }

    [EfinFileRowIndex(11, 25)]
    public string cliwb_description { get; set; }

    [EfinFileRowIndex(26, 29)]
    public string cliwb_year { get; set; }

    [EfinFileRowIndex(30, 31)]
    public string cliwb_period { get; set; }

    [EfinFileRowIndex(32, 38)]
    public string cliwb_no_invoices { get; set; }

    [EfinFileRowIndex(46, 65)]
    public string cliwb_user { get; set; }

    [EfinFileRowIndex(86, 94)]
    public string cliwb_entry_date { get; set; }

    [EfinFileRowIndex(98, 98)]
    public string cliwb_default_prefix { get; set; }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
