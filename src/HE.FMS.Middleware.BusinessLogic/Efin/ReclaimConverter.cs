using System.Globalization;
using HE.FMS.Middleware.Contract.Constants;
using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Contract.Reclaims.Efin;
using HE.FMS.Middleware.Providers.Common;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public class ReclaimConverter : IReclaimConverter
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IEfinLookupCacheService _lookupCacheService;

    public ReclaimConverter(IDateTimeProvider dateTimeProvider, IEfinLookupCacheService lookupCacheService)
    {
        _dateTimeProvider = dateTimeProvider;
        _lookupCacheService = lookupCacheService;
    }

    public async Task<ReclaimItem> CreateItems(ReclaimPaymentRequest reclaimPaymentRequest)
    {
        return new ReclaimItem
        {
            CliIwIlt = await CreateCliIwIlt(reclaimPaymentRequest),
            CliIwIna = await CreateCliIwIna(reclaimPaymentRequest),
            CliIwInl = await CreateCliIwInl(reclaimPaymentRequest),
            CliIwInv = await CreateCliIwInv(reclaimPaymentRequest),
            CliIwItl = await CreateCliIwItl(reclaimPaymentRequest),
        };
    }

    public async Task<CLI_IW_BAT> CreateBatch(IEnumerable<ReclaimItem> reclaims, string batchRef)
    {
        ArgumentNullException.ThrowIfNull(reclaims);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Default.ReclaimDefault);

        return new CLI_IW_BAT()
        {
            cliwb_sub_ledger = defaultDictionary[nameof(CLI_IW_BAT.cliwb_sub_ledger)],
            cliwb_batch_ref = batchRef,
            cliwb_description = defaultDictionary[nameof(CLI_IW_BAT.cliwb_description)],
            cliwb_year = (DateTime.Now.Month is >= 1 and <= 3 ? DateTime.Now.Year - 1 : DateTime.Now.Year).ToString(CultureInfo.InvariantCulture),
            cliwb_period = DateTime.UtcNow.Month.ToString(CultureInfo.InvariantCulture),
            cliwb_no_invoices = reclaims.Count().ToString(CultureInfo.InvariantCulture),
            cliwb_user = defaultDictionary[nameof(CLI_IW_BAT.cliwb_user)],
            cliwb_entry_date = DateTime.UtcNow.ToString("d-MMM-yy", CultureInfo.InvariantCulture),
            cliwb_default_prefix = defaultDictionary[nameof(CLI_IW_BAT.cliwb_default_prefix)],
        };
    }

    public async Task<CLI_IW_ILT> CreateCliIwIlt(ReclaimPaymentRequest reclaimPayment)
    {
        ArgumentNullException.ThrowIfNull(reclaimPayment);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Default.ReclaimDefault);

        return new CLI_IW_ILT()
        {
            cliwt_sub_ledger_id = defaultDictionary[nameof(CLI_IW_ILT.cliwt_sub_ledger_id)],
            cliwt_inv_ref = reclaimPayment.Application.AllocationId,
            cliwt_batch_ref = string.Empty,
            cliwt_item_sequence = defaultDictionary[nameof(CLI_IW_ILT.cliwt_item_sequence)],
            cliwt_print_sequence = defaultDictionary[nameof(CLI_IW_ILT.cliwt_print_sequence)],
            cliwt_text = defaultDictionary[nameof(CLI_IW_ILT.cliwt_text)],
        };
    }

    public async Task<CLI_IW_INA> CreateCliIwIna(ReclaimPaymentRequest reclaimPayment)
    {
        ArgumentNullException.ThrowIfNull(reclaimPayment);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Default.ReclaimDefault);
        var regionLookup = await _lookupCacheService.GetValue(EfinConstants.Default.RegionLookup);

        return new CLI_IW_INA()
        {
            cliwa_sub_ledger_id = defaultDictionary[nameof(CLI_IW_INA.cliwa_sub_ledger_id)],
            cliwa_batch_ref = string.Empty,
            cliwa_inv_ref = reclaimPayment.Application.AllocationId,
            cliwa_item_sequence = defaultDictionary[nameof(CLI_IW_INA.cliwa_item_sequence)],
            cliwa_cost_centre = regionLookup[reclaimPayment.Application.Region],
            cliwa_account = reclaimPayment.Organisation.PartnerType,
            cliwa_activity = reclaimPayment.Application.EfinTenure,
            cliwa_job = reclaimPayment.Application.Id,
            cliwa_amount = reclaimPayment.Reclaim.Amount.ToString("F", CultureInfo.InvariantCulture),
            cliwa_uom = defaultDictionary[nameof(CLI_IW_INA.cliwa_uom)],
            cliwa_pre_pay_yn = defaultDictionary[nameof(CLI_IW_INA.cliwa_pre_pay_yn)],
        };
    }

    public async Task<CLI_IW_INL> CreateCliIwInl(ReclaimPaymentRequest reclaimPayment)
    {
        ArgumentNullException.ThrowIfNull(reclaimPayment);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Default.ReclaimDefault);

        return new CLI_IW_INL()
        {
            cliwl_sub_ledger_id = defaultDictionary[nameof(CLI_IW_INL.cliwl_sub_ledger_id)],
            cliwl_inv_ref = reclaimPayment.Application.AllocationId,
            cliwl_batch_ref = string.Empty,
            cliwl_item_sequence = defaultDictionary[nameof(CLI_IW_INL.cliwl_item_sequence)],
            cliwl_product_id = defaultDictionary[nameof(CLI_IW_INL.cliwl_product_id)],
            cliwl_goods_value = reclaimPayment.Reclaim.Amount.ToString("F", CultureInfo.InvariantCulture),
            cliwl_vat_code = reclaimPayment.Application.VatCode,
            cliwl_vat_amount = (reclaimPayment.Reclaim.Amount * reclaimPayment.Application.VatRate).ToString("F", CultureInfo.InvariantCulture),
            cliwl_line_ref = defaultDictionary[nameof(CLI_IW_INL.cliwl_line_ref)],
        };
    }

    public async Task<CLI_IW_INV> CreateCliIwInv(ReclaimPaymentRequest reclaimPayment)
    {
        ArgumentNullException.ThrowIfNull(reclaimPayment);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Default.ReclaimDefault);
        var milestoneLookup = await _lookupCacheService.GetValue(EfinConstants.Default.MilestoneLookup);
        var regionLookup = await _lookupCacheService.GetValue(EfinConstants.Default.RegionLookup);

        return new CLI_IW_INV()
        {
            cliwi_sub_ledger_id = defaultDictionary[nameof(CLI_IW_INV.cliwi_sub_ledger_id)],
            cliwi_inv_ref = reclaimPayment.Application.AllocationId,
            cliwi_batch_ref = string.Empty,
            cliwi_invoice_to_id = reclaimPayment.Reclaim.Id,
            cliwi_net_amount = reclaimPayment.Reclaim.Amount.ToString("F", CultureInfo.InvariantCulture),
            cliwi_their_ref = reclaimPayment.Application.AllocationId,
            cliwi_trans_type = defaultDictionary[nameof(CLI_IW_INV.cliwi_trans_type)],
            cliwi_date = _dateTimeProvider.UtcNow.ToString("d-MMM-yy", CultureInfo.InvariantCulture),
            cliwi_terms_code = defaultDictionary[nameof(CLI_IW_INV.cliwi_terms_code)],
            cliwi_cost_centre = regionLookup[reclaimPayment.Application.Region],
            cliwi_job = reclaimPayment.Application.Id,
            cliwi_account = reclaimPayment.Organisation.PartnerType,
            cliwi_activity = reclaimPayment.Application.EfinTenure,
            cliwi_entry_date = _dateTimeProvider.UtcNow.ToString("F", CultureInfo.InvariantCulture),
            cliwi_invoice_prefix = defaultDictionary[nameof(CLI_IW_INV.cliwi_invoice_prefix)],
            cliwi_tax_point = _dateTimeProvider.UtcNow.ToString("F", CultureInfo.InvariantCulture),
            cliwi_description = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", milestoneLookup[reclaimPayment.Reclaim.Milestone], reclaimPayment.Reclaim.Id, reclaimPayment.Application.Id),
        };
    }

    public async Task<CLI_IW_ITL> CreateCliIwItl(ReclaimPaymentRequest reclaimPayment)
    {
        ArgumentNullException.ThrowIfNull(reclaimPayment);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Default.ReclaimDefault);
        var milestoneLookup = await _lookupCacheService.GetValue(EfinConstants.Default.MilestoneLookup);

        return new CLI_IW_ITL()
        {
            cliwx_sub_ledger_id = defaultDictionary[nameof(CLI_IW_ITL.cliwx_sub_ledger_id)],
            cliwx_batch_ref = string.Empty,
            cliwx_inv_ref = reclaimPayment.Application.AllocationId,
            cliwx_line_no = defaultDictionary[nameof(CLI_IW_ITL.cliwx_line_no)],
            cliwx_header_footer = defaultDictionary[nameof(CLI_IW_ITL.cliwx_header_footer)],
            cliwx_text = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", milestoneLookup[reclaimPayment.Reclaim.Milestone], reclaimPayment.Reclaim.Id, reclaimPayment.Application.Id),
        };
    }
}
