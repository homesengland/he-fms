using System.Globalization;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Reclaims.Efin;
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
public class CLI_IW_BAT
{
    public const string FileName = "cpiw_bt";

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

    public static CLI_IW_BAT Create(IEnumerable<ReclaimItem> reclaims, string batchRef)
    {
        ArgumentNullException.ThrowIfNull(reclaims);

        return new CLI_IW_BAT()
        {
            cliwb_sub_ledger = EfinConstants.Default.Reclaim.SubLedger,

            cliwb_batch_ref = batchRef,
            cliwb_description = EfinConstants.Default.Reclaim.Description,
            cliwb_year = (DateTime.Now.Month is >= 1 and <= 3 ? DateTime.Now.Year - 1 : DateTime.Now.Year).ToString(CultureInfo.InvariantCulture),
            cliwb_period = DateTime.UtcNow.Month.ToString(CultureInfo.InvariantCulture),
            cliwb_no_invoices = reclaims.Count().ToString(CultureInfo.InvariantCulture),
            cliwb_user = EfinConstants.Default.Reclaim.User,
            cliwb_entry_date = DateTime.UtcNow.ToString("d-MMM-yy", CultureInfo.InvariantCulture),
            cliwb_default_prefix = EfinConstants.Default.Reclaim.Prefix,
        };
    }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
