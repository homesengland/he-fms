using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Reclaims.Efin;
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
public class CLI_IW_INA
{
    public string cliwa_sub_ledger_id { get; set; }

    public string cliwa_batch_ref { get; set; }

    public string cliwa_inv_ref { get; set; }

    public string cliwa_item_sequence { get; set; }

    public string cliwa_cost_centre { get; set; }

    public string cliwa_account { get; set; }

    public string cliwa_activity { get; set; }

    public string cliwa_job { get; set; }

    public string cliwa_amount { get; set; }

    public string cliwa_uom { get; set; }

    public string cliwa_pre_pay_yn { get; set; }

    public static CLI_IW_INA Create(CLI_IW_BAT batch, ReclaimPaymentRequest reclaimPayment)
    {
        return new CLI_IW_INA()
        {
            cliwa_sub_ledger_id = EfinConstants.Default.Reclaim.SubLedger,
            cliwa_batch_ref = batch.cliwb_batch_ref,

            // cliwa_inv_ref = <unique_value>
            cliwa_item_sequence = EfinConstants.Default.Reclaim.ItemSequence,
            cliwa_cost_centre = reclaimPayment.Application.EfinRegion,
            cliwa_account = reclaimPayment.Organisation.PartnerType,
            cliwa_activity = reclaimPayment.Application.EfinTenure,
            cliwa_job = reclaimPayment.Application.Id,
            cliwa_amount = reclaimPayment.Reclaim.Amount.ToString("F", CultureInfo.InvariantCulture),
            cliwa_uom = EfinConstants.Default.Reclaim.UOM,
            cliwa_pre_pay_yn = EfinConstants.Default.Reclaim.PrePay,
        };
    }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
