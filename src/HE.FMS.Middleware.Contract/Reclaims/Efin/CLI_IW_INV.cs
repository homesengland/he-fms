using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Reclaims.Efin;
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
public class CLI_IW_INV
{
    public string cliwi_sub_ledger_id { get; set; }

    public string cliwi_inv_ref { get; set; }

    public string cliwi_batch_ref { get; set; }

    public string cliwi_invoice_to_id { get; set; }

    public string cliwi_net_amount { get; set; }

    public string cliwi_their_ref { get; set; }

    public string cliwi_trans_type { get; set; }

    public string cliwi_date { get; set; }

    public string cliwi_terms_code { get; set; }

    public string cliwi_cost_centre { get; set; }

    public string cliwi_job { get; set; }

    public string cliwi_account { get; set; }

    public string cliwi_activity { get; set; }

    public string cliwi_entry_date { get; set; }

    public string cliwi_invoice_prefix { get; set; }

    public string cliwi_tax_point { get; set; }

    public string cliwi_description { get; set; }

    public static CLI_IW_INV Create(CLI_IW_BAT batch, ReclaimPaymentRequest reclaimPayment)
    {
        return new CLI_IW_INV()
        {
            cliwi_sub_ledger_id = EfinConstants.Default.Reclaim.SubLedger,

            //cliwi_inv_ref = <unique_value>
            cliwi_batch_ref = batch.cliwb_batch_ref,
            cliwi_invoice_to_id = reclaimPayment.Reclaim.Id,
            cliwi_net_amount = reclaimPayment.Reclaim.Amount.ToString("F", CultureInfo.InvariantCulture),
            cliwi_their_ref = reclaimPayment.Application.Id,
            cliwi_trans_type = EfinConstants.Default.Reclaim.TransType,
            cliwi_date = DateTime.UtcNow.ToString("d-MMM-yy", CultureInfo.InvariantCulture),
            cliwi_terms_code = EfinConstants.Default.Reclaim.TermsCode,
            cliwi_cost_centre = reclaimPayment.Application.EfinRegion,
            cliwi_job = reclaimPayment.Application.Id,
            cliwi_account = reclaimPayment.Organisation.PartnerType,
            cliwi_activity = reclaimPayment.Application.EfinTenure,
            cliwi_entry_date = DateTime.UtcNow.ToString("F", CultureInfo.InvariantCulture),
            cliwi_invoice_prefix = EfinConstants.Default.Reclaim.Prefix,
            cliwi_tax_point = DateTime.UtcNow.ToString("F", CultureInfo.InvariantCulture),
            cliwi_description = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", reclaimPayment.Reclaim.Milestone[..3], reclaimPayment.Reclaim.Id, reclaimPayment.Application.Id),
        };
    }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
