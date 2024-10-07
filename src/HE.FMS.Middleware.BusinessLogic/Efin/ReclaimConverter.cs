using System.Globalization;
using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Contract.Reclaims.Efin;
using HE.FMS.Middleware.Providers.Common;
using EfinConstants = HE.FMS.Middleware.BusinessLogic.Constants.EfinConstants;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public class ReclaimConverter : PaymentConverter, IReclaimConverter
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

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Lookups.ReclaimDefault);

        return new CLI_IW_BAT()
        {
            cliwb_sub_ledger = defaultDictionary[nameof(CLI_IW_BAT.cliwb_sub_ledger)],
            cliwb_batch_ref = batchRef,
            cliwb_description = defaultDictionary[nameof(CLI_IW_BAT.cliwb_description)],
            cliwb_year = GetAccountingYear(_dateTimeProvider.UtcNow).ToString(CultureInfo.InvariantCulture),
            cliwb_period = GetAccountingPeriod(_dateTimeProvider.UtcNow).ToString(CultureInfo.InvariantCulture),
            cliwb_no_invoices = reclaims.Count().ToString(CultureInfo.InvariantCulture),
            cliwb_user = defaultDictionary[nameof(CLI_IW_BAT.cliwb_user)],
            cliwb_entry_date = _dateTimeProvider.UtcNow.ToString(DateFormat, CultureInfo.InvariantCulture),
            cliwb_default_prefix = defaultDictionary[nameof(CLI_IW_BAT.cliwb_default_prefix)],
        };
    }

    public async Task<CLI_IW_ILT> CreateCliIwIlt(ReclaimPaymentRequest reclaimPayment)
    {
        ArgumentNullException.ThrowIfNull(reclaimPayment);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Lookups.ReclaimDefault);

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

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Lookups.ReclaimDefault);
        var regionLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.RegionLookup);
        var tenureLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.TenureLookup);
        var partnerTypeLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.PartnerTypeLookup);

        return new CLI_IW_INA()
        {
            cliwa_sub_ledger_id = defaultDictionary[nameof(CLI_IW_INA.cliwa_sub_ledger_id)],
            cliwa_batch_ref = string.Empty,
            cliwa_inv_ref = reclaimPayment.Application.AllocationId,
            cliwa_item_sequence = defaultDictionary[nameof(CLI_IW_INA.cliwa_item_sequence)],
            cliwa_cost_centre = regionLookup[reclaimPayment.Application.Region.ToString()],
            cliwa_account = reclaimPayment.Reclaim.Amount != 0 ?
                partnerTypeLookup[$"Reclaim_{nameof(ReclaimPaymentRequest.Reclaim.Amount)}_{reclaimPayment.Account.PartnerType}"] :
                partnerTypeLookup[$"Reclaim_{nameof(ReclaimPaymentRequest.Reclaim.InterestAmount)}_{reclaimPayment.Account.PartnerType}"],
            cliwa_activity = tenureLookup[reclaimPayment.Application.Tenure.ToString()],
            cliwa_job = defaultDictionary[nameof(CLI_IW_INA.cliwa_job)],
            cliwa_amount = reclaimPayment.Reclaim.Amount.ToString(DecimalFormat, CultureInfo.InvariantCulture),
            cliwa_uom = defaultDictionary[nameof(CLI_IW_INA.cliwa_uom)],
            cliwa_pre_pay_yn = defaultDictionary[nameof(CLI_IW_INA.cliwa_pre_pay_yn)],
        };
    }

    public async Task<CLI_IW_INL> CreateCliIwInl(ReclaimPaymentRequest reclaimPayment)
    {
        ArgumentNullException.ThrowIfNull(reclaimPayment);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Lookups.ReclaimDefault);

        return new CLI_IW_INL()
        {
            cliwl_sub_ledger_id = defaultDictionary[nameof(CLI_IW_INL.cliwl_sub_ledger_id)],
            cliwl_inv_ref = reclaimPayment.Application.AllocationId,
            cliwl_batch_ref = string.Empty,
            cliwl_item_sequence = defaultDictionary[nameof(CLI_IW_INL.cliwl_item_sequence)],
            cliwl_product_id = defaultDictionary[nameof(CLI_IW_INL.cliwl_product_id)],
            cliwl_goods_value = reclaimPayment.Reclaim.Amount.ToString(DecimalFormat, CultureInfo.InvariantCulture),
            cliwl_vat_code = ((int)reclaimPayment.Application.VatCode).ToString("D2", CultureInfo.InvariantCulture),
            cliwl_vat_amount = CalculateVatAmount(reclaimPayment.Reclaim.Amount, reclaimPayment.Application.VatRate).ToString(DecimalFormat, CultureInfo.InvariantCulture),
            cliwl_line_ref = defaultDictionary[nameof(CLI_IW_INL.cliwl_line_ref)],
        };
    }

    public async Task<CLI_IW_INV> CreateCliIwInv(ReclaimPaymentRequest reclaimPayment)
    {
        ArgumentNullException.ThrowIfNull(reclaimPayment);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Lookups.ReclaimDefault);
        var milestoneLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.MilestoneLookup);
        var regionLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.RegionLookup);
        var tenureLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.TenureLookup);
        var partnerTypeLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.PartnerTypeLookup);

        return new CLI_IW_INV()
        {
            cliwi_sub_ledger_id = defaultDictionary[nameof(CLI_IW_INV.cliwi_sub_ledger_id)],
            cliwi_inv_ref = reclaimPayment.Application.AllocationId,
            cliwi_batch_ref = string.Empty,
            cliwi_invoice_to_id = reclaimPayment.Account.ProviderId,
            cliwi_net_amount = reclaimPayment.Reclaim.Amount.ToString(DecimalFormat, CultureInfo.InvariantCulture),
            cliwi_their_ref = reclaimPayment.Application.AllocationId,
            cliwi_trans_type = defaultDictionary[nameof(CLI_IW_INV.cliwi_trans_type)],
            cliwi_date = _dateTimeProvider.UtcNow.ToString(DateFormat, CultureInfo.InvariantCulture),
            cliwi_terms_code = defaultDictionary[nameof(CLI_IW_INV.cliwi_terms_code)],
            cliwi_cost_centre = regionLookup[reclaimPayment.Application.Region.ToString()],
            cliwi_job = defaultDictionary[nameof(CLI_IW_INV.cliwi_job)],
            cliwi_account = reclaimPayment.Reclaim.Amount != 0 ?
                partnerTypeLookup[$"Reclaim_{nameof(ReclaimPaymentRequest.Reclaim.Amount)}_{reclaimPayment.Account.PartnerType}"] :
                partnerTypeLookup[$"Reclaim_{nameof(ReclaimPaymentRequest.Reclaim.InterestAmount)}_{reclaimPayment.Account.PartnerType}"],
            cliwi_activity = tenureLookup[reclaimPayment.Application.Tenure.ToString()],
            cliwi_entry_date = _dateTimeProvider.UtcNow.ToString(DateFormat, CultureInfo.InvariantCulture),
            cliwi_invoice_prefix = defaultDictionary[nameof(CLI_IW_INV.cliwi_invoice_prefix)],
            cliwi_tax_point = _dateTimeProvider.UtcNow.ToString(DateFormat, CultureInfo.InvariantCulture),
            cliwi_description = GetDescription(reclaimPayment.Reclaim, reclaimPayment.Application, milestoneLookup),
        };
    }

    public async Task<CLI_IW_ITL> CreateCliIwItl(ReclaimPaymentRequest reclaimPayment)
    {
        ArgumentNullException.ThrowIfNull(reclaimPayment);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Lookups.ReclaimDefault);
        var milestoneLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.MilestoneLookup);

        return new CLI_IW_ITL()
        {
            cliwx_sub_ledger_id = defaultDictionary[nameof(CLI_IW_ITL.cliwx_sub_ledger_id)],
            cliwx_batch_ref = string.Empty,
            cliwx_inv_ref = reclaimPayment.Application.AllocationId,
            cliwx_line_no = defaultDictionary[nameof(CLI_IW_ITL.cliwx_line_no)],
            cliwx_header_footer = defaultDictionary[nameof(CLI_IW_ITL.cliwx_header_footer)],
            cliwx_text = GetDescription(reclaimPayment.Reclaim, reclaimPayment.Application, milestoneLookup),
        };
    }
}
