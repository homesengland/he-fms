using System.Globalization;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Constants;

#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
namespace HE.FMS.Middleware.Contract.Claims.Efin;
[EfinFileRowSize(201)]
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

    public static CLCLB_Batch Create(IEnumerable<ClaimItem> claims)
    {
        ArgumentNullException.ThrowIfNull(claims);

        var culture = new CultureInfo("en-GB");
        return new CLCLB_Batch()
        {
            clb_sub_ledger = EfinConstants.Default.Claim.SubLedger,

            // clb_batch_ref = <unique_value>,
            clb_year = (DateTime.UtcNow.Month is >= 1 and <= 3 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year).ToString(CultureInfo.InvariantCulture),
            clb_period = DateTime.UtcNow.Month.ToString(CultureInfo.InvariantCulture),
            clb_net_amount = claims.Sum(x => decimal.Parse(x.CliInvoice.cli_net_amount, NumberStyles.Any, culture)).ToString("F", CultureInfo.InvariantCulture),
            clb_vat_amount = EfinConstants.Default.Claim.Amount,
            clb_no_invoices = claims.Count().ToString(CultureInfo.InvariantCulture),
            clb_quantity = claims.Count().ToString(CultureInfo.InvariantCulture),
            clb_user = EfinConstants.Default.Claim.User,
            clb_grouping = EfinConstants.Default.Claim.Grouping,
            clb_entry_date = DateTime.UtcNow.ToString("d-MMM-yy", CultureInfo.InvariantCulture),
        };
    }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
