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
public class CLI_IW_ILT
{
    public string cliwt_sub_ledger_id { get; set; }

    public string cliwt_batch_ref { get; set; }

    public string cliwt_inv_ref { get; set; }

    public string cliwt_item_sequence { get; set; }

    public string cliwt_print_sequence { get; set; }

    public string cliwt_text { get; set; }

    public static CLI_IW_ILT Create(CLI_IW_BAT batch, ReclaimPaymentRequest reclaimPayment)
    {
        return new CLI_IW_ILT()
        {
            cliwt_sub_ledger_id = EfinConstants.Default.Reclaim.SubLedger,

            // cliwt_inv_ref = <unique_value>
            cliwt_batch_ref = batch.cliwb_batch_ref,
            cliwt_item_sequence = EfinConstants.Default.Reclaim.ItemSequence,
            cliwt_print_sequence = EfinConstants.Default.Reclaim.PrintSequence,
            cliwt_text = EfinConstants.Default.Reclaim.Text,
        };
    }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
