using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HE.FMS.Middleware.Contract.Constants;

#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
namespace HE.FMS.Middleware.Contract.Claims.Efin;
public class CLCLB_Batch
{
    public string clb_sub_ledger { get; set; }

    public string clb_batch_ref { get; set; }

    public string clb_year { get; set; }

    public string clb_period { get; set; }

    public string clb_net_amount { get; set; }

    public string clb_vat_amount { get; set; }

    public string clb_no_invoices { get; set; }

    public string clb_user { get; set; }

    public string clb_quantity { get; set; }

    public string clb_grouping { get; set; }

    public string clb_entry_date { get; set; }

    public static CLCLB_Batch Create(IEnumerable<ClaimPaymentRequest> claimPayments)
    {
        return new CLCLB_Batch()
        {
            clb_sub_ledger = EfinConstants.Default.Claim.SubLedger,

            // clb_batch_ref = <unique_value>,
            clb_year = (DateTime.UtcNow.Month is >= 1 and <= 3 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year).ToString(CultureInfo.InvariantCulture),
            clb_period = DateTime.UtcNow.Month.ToString(CultureInfo.InvariantCulture),
            clb_net_amount = claimPayments.Sum(x => x.Claim.Amount).ToString("F", CultureInfo.InvariantCulture),
            clb_vat_amount = EfinConstants.Default.Claim.Amount,
            clb_no_invoices = claimPayments.Count().ToString(CultureInfo.InvariantCulture),
            clb_quantity = claimPayments.Count().ToString(CultureInfo.InvariantCulture),
            clb_user = EfinConstants.Default.Claim.User,
            clb_grouping = EfinConstants.Default.Claim.Grouping,
            clb_entry_date = DateTime.UtcNow.ToString("d-MMM-yy", CultureInfo.InvariantCulture),
        };
    }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase