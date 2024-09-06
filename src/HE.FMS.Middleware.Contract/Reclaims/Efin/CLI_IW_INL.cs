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
public class CLI_IW_INL
{
    public string cliwl_sub_ledger_id { get; set; }

    public string cliwl_batch_ref { get; set; }

    public string cliwl_inv_ref { get; set; }

    public string cliwl_item_sequence { get; set; }

    public string cliwl_product_id { get; set; }

    public string cliwl_goods_value { get; set; }

    public string cliwl_vat_code { get; set; }

    public string cliwl_vat_amount { get; set; }

    public string cliwl_line_ref { get; set; }

    public static CLI_IW_INL Create(CLI_IW_BAT batch, ReclaimPaymentRequest reclaimPayment)
    {
        return new CLI_IW_INL()
        {
            cliwl_sub_ledger_id = EfinConstants.Default.Reclaim.SubLedger,

            // cliwl_inv_ref = <unique_value>
            cliwl_batch_ref = batch.cliwb_batch_ref,
            cliwl_item_sequence = EfinConstants.Default.Reclaim.ItemSequence,
            cliwl_product_id = EfinConstants.Default.Reclaim.Product,
            cliwl_goods_value = reclaimPayment.Reclaim.Amount.ToString("F", CultureInfo.InvariantCulture),
            cliwl_vat_code = reclaimPayment.Application.VatCode,
            cliwl_vat_amount = (reclaimPayment.Reclaim.Amount * reclaimPayment.Application.VatRate).ToString("F", CultureInfo.InvariantCulture),
            cliwl_line_ref = EfinConstants.Default.Reclaim.Line,
        };
    }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
