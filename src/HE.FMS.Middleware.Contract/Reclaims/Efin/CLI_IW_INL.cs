using System.Globalization;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Reclaims.Efin;
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
[EfinFileRowSize(147)]
public class CLI_IW_INL
{
    [EfinFileRowIndex(1, 3)]
    public string cliwl_sub_ledger_id { get; set; }

    [EfinFileRowIndex(4, 10)]
    public string cliwl_batch_ref { get; set; }

    [EfinFileRowIndex(11, 18)]
    public string cliwl_inv_ref { get; set; }

    [EfinFileRowIndex(19, 23)]
    public string cliwl_item_sequence { get; set; }

    [EfinFileRowIndex(24, 43)]
    public string cliwl_product_id { get; set; }

    [EfinFileRowIndex(44, 59)]
    public string cliwl_goods_value { get; set; }

    [EfinFileRowIndex(108, 109)]
    public string cliwl_vat_code { get; set; }

    [EfinFileRowIndex(118, 133)]
    public string cliwl_vat_amount { get; set; }

    [EfinFileRowIndex(142, 147)]
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

    public static CLI_IW_INL Create(ReclaimPaymentRequest reclaimPayment, string invoiceRef)
    {
        return new CLI_IW_INL()
        {
            cliwl_sub_ledger_id = EfinConstants.Default.Reclaim.SubLedger,

            cliwl_inv_ref = invoiceRef,
            cliwl_batch_ref = string.Empty,
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
