using System.Globalization;
using HE.FMS.Middleware.Contract.Constants;
using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Contract.Reclaims.Efin;
using HE.FMS.Middleware.Providers.Common;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public class ReclaimConverter : IReclaimConverter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public ReclaimConverter(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public ReclaimItem Convert(ReclaimPaymentRequest reclaimPaymentRequest)
    {
        return new ReclaimItem
        {
            CliIwIlt = CreateCliIwIlt(reclaimPaymentRequest),
            CliIwIna = CreateCliIwIna(reclaimPaymentRequest),
            CliIwInl = CreateCliIwInl(reclaimPaymentRequest),
            CliIwInv = CreateCliIwInv(reclaimPaymentRequest),
            CliIwItl = CreateCliIwItl(reclaimPaymentRequest),
        };
    }

    public CLI_IW_ILT CreateCliIwIlt(ReclaimPaymentRequest reclaimPayment)
    {
        return new CLI_IW_ILT()
        {
            cliwt_sub_ledger_id = EfinConstants.Default.Reclaim.SubLedger,
            cliwt_inv_ref = reclaimPayment.Application.AllocationId,
            cliwt_batch_ref = string.Empty,
            cliwt_item_sequence = EfinConstants.Default.Reclaim.ItemSequence,
            cliwt_print_sequence = EfinConstants.Default.Reclaim.PrintSequence,
            cliwt_text = EfinConstants.Default.Reclaim.Text,
        };
    }

    public CLI_IW_INA CreateCliIwIna(ReclaimPaymentRequest reclaimPayment)
    {
        return new CLI_IW_INA()
        {
            cliwa_sub_ledger_id = EfinConstants.Default.Reclaim.SubLedger,
            cliwa_batch_ref = string.Empty,
            cliwa_inv_ref = reclaimPayment.Application.AllocationId,
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

    public CLI_IW_INL CreateCliIwInl(ReclaimPaymentRequest reclaimPayment)
    {
        return new CLI_IW_INL()
        {
            cliwl_sub_ledger_id = EfinConstants.Default.Reclaim.SubLedger,
            cliwl_inv_ref = reclaimPayment.Application.AllocationId,
            cliwl_batch_ref = string.Empty,
            cliwl_item_sequence = EfinConstants.Default.Reclaim.ItemSequence,
            cliwl_product_id = EfinConstants.Default.Reclaim.Product,
            cliwl_goods_value = reclaimPayment.Reclaim.Amount.ToString("F", CultureInfo.InvariantCulture),
            cliwl_vat_code = reclaimPayment.Application.VatCode,
            cliwl_vat_amount = (reclaimPayment.Reclaim.Amount * reclaimPayment.Application.VatRate).ToString("F", CultureInfo.InvariantCulture),
            cliwl_line_ref = EfinConstants.Default.Reclaim.Line,
        };
    }

    public CLI_IW_INV CreateCliIwInv(ReclaimPaymentRequest reclaimPayment)
    {
        return new CLI_IW_INV()
        {
            cliwi_sub_ledger_id = EfinConstants.Default.Reclaim.SubLedger,
            cliwi_inv_ref = reclaimPayment.Application.AllocationId,
            cliwi_batch_ref = string.Empty,
            cliwi_invoice_to_id = reclaimPayment.Reclaim.Id,
            cliwi_net_amount = reclaimPayment.Reclaim.Amount.ToString("F", CultureInfo.InvariantCulture),
            cliwi_their_ref = reclaimPayment.Application.AllocationId,
            cliwi_trans_type = EfinConstants.Default.Reclaim.TransType,
            cliwi_date = _dateTimeProvider.UtcNow.ToString("d-MMM-yy", CultureInfo.InvariantCulture),
            cliwi_terms_code = EfinConstants.Default.Reclaim.TermsCode,
            cliwi_cost_centre = reclaimPayment.Application.EfinRegion,
            cliwi_job = reclaimPayment.Application.Id,
            cliwi_account = reclaimPayment.Organisation.PartnerType,
            cliwi_activity = reclaimPayment.Application.EfinTenure,
            cliwi_entry_date = _dateTimeProvider.UtcNow.ToString("F", CultureInfo.InvariantCulture),
            cliwi_invoice_prefix = EfinConstants.Default.Reclaim.Prefix,
            cliwi_tax_point = _dateTimeProvider.UtcNow.ToString("F", CultureInfo.InvariantCulture),
            cliwi_description = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", reclaimPayment.Reclaim.Milestone[..3], reclaimPayment.Reclaim.Id, reclaimPayment.Application.Id),
        };
    }

    public CLI_IW_ITL CreateCliIwItl(ReclaimPaymentRequest reclaimPayment)
    {
        return new CLI_IW_ITL()
        {
            cliwx_sub_ledger_id = EfinConstants.Default.Reclaim.SubLedger,
            cliwx_batch_ref = string.Empty,
            cliwx_inv_ref = reclaimPayment.Application.AllocationId,
            cliwx_line_no = EfinConstants.Default.Reclaim.Line,
            cliwx_header_footer = EfinConstants.Default.Reclaim.HeaderFooter,
            cliwx_text = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", reclaimPayment.Reclaim.Milestone[..3], reclaimPayment.Reclaim.Id, reclaimPayment.Application.Id),
        };
    }
}
