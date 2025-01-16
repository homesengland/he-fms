using System.Globalization;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Claims.Efin;
using HE.FMS.Middleware.Providers.Common;
using EfinConstants = HE.FMS.Middleware.BusinessLogic.Constants.EfinConstants;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public class ClaimConverter : PaymentConverter, IClaimConverter
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IEfinLookupCacheService _lookupCacheService;

    public ClaimConverter(IDateTimeProvider dateTimeProvider, IEfinLookupCacheService lookupCacheService)
    {
        _dateTimeProvider = dateTimeProvider;
        _lookupCacheService = lookupCacheService;
    }

    public async Task<ClaimItem> CreateItems(ClaimPaymentRequest claimPaymentRequest)
    {
        return new ClaimItem
        {
            CliInvoice = await CreateCliInvoice(claimPaymentRequest),
            ClaInvoiceAnalysis = await CreateClaInvoiceAnalysis(claimPaymentRequest),
        };
    }

    public async Task<CLCLB_Batch> CreateBatch(IEnumerable<ClaimItem> claims, string batchRef)
    {
        ArgumentNullException.ThrowIfNull(claims);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Lookups.ClaimDefault);

        var culture = new CultureInfo("en-GB");
        return new CLCLB_Batch()
        {
            clb_sub_ledger = defaultDictionary[nameof(CLCLB_Batch.clb_sub_ledger)],
            clb_batch_ref = batchRef,
            clb_year = GetAccountingYear(_dateTimeProvider.UtcNow).ToString(CultureInfo.InvariantCulture),
            clb_period = GetAccountingPeriod(_dateTimeProvider.UtcNow).ToString(CultureInfo.InvariantCulture),
            clb_net_amount = claims.Sum(x => decimal.Parse(x.CliInvoice.cli_net_amount, NumberStyles.Any, culture)).ToString(DecimalFormat, CultureInfo.InvariantCulture),
            clb_vat_amount = claims.Sum(x => decimal.Parse(x.CliInvoice.cli_vat, NumberStyles.Any, culture)).ToString(DecimalFormat, CultureInfo.InvariantCulture),
            clb_no_invoices = claims.Count().ToString(CultureInfo.InvariantCulture),
            clb_quantity = claims.Count().ToString(CultureInfo.InvariantCulture),
            clb_user = defaultDictionary[nameof(CLCLB_Batch.clb_user)],
            clb_grouping = defaultDictionary[nameof(CLCLB_Batch.clb_grouping)],
            clb_entry_date = _dateTimeProvider.UtcNow.ToString(DateFormat, CultureInfo.InvariantCulture),
        };
    }

    public async Task<CLI_Invoice> CreateCliInvoice(ClaimPaymentRequest claimPayment)
    {
        ArgumentNullException.ThrowIfNull(claimPayment);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Lookups.ClaimDefault);
        var milestoneLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.MilestoneLookup);
        var regionLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.RegionLookup);
        var tenureLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.TenureLookup);

        return new CLI_Invoice()
        {
            cli_sub_ledger = defaultDictionary[nameof(CLI_Invoice.cli_sub_ledger)],
            cli_inv_ref = claimPayment.Claim.InvoiceId,
            cli_batch_ref = string.Empty,
            cli_cfacs_customer = claimPayment.Account.ProviderId,
            cli_net_amount = claimPayment.Claim.Amount.ToString(DecimalFormat, CultureInfo.InvariantCulture),
            cli_vat = CalculateVatAmount(claimPayment.Claim.Amount, claimPayment.Application.VatRate).ToString(DecimalFormat, CultureInfo.InvariantCulture),
            cli_gross = CalculateGrossAmount(claimPayment.Claim.Amount, claimPayment.Application.VatRate).ToString(DecimalFormat, CultureInfo.InvariantCulture),
            cli_volume = defaultDictionary[nameof(CLI_Invoice.cli_volume)],
            cli_uom = defaultDictionary[nameof(CLI_Invoice.cli_uom)],
            cli_our_ref_2 = claimPayment.Application.AllocationId,
            cli_their_ref = claimPayment.Claim.InvoiceId,
            cli_trans_type = defaultDictionary[nameof(CLI_Invoice.cli_trans_type)],
            cli_date = claimPayment.Claim.ApprovedOn.ToString(DateFormat, CultureInfo.InvariantCulture),
            cli_description = GetDescription(claimPayment.Claim, claimPayment.Claim.Milestone, claimPayment.Application, milestoneLookup),
            cli_terms_code = defaultDictionary[nameof(CLI_Invoice.cli_terms_code)],
            cli_due_date = claimPayment.Claim.ApprovedOn.AddDays(7).ToString(DateFormat, CultureInfo.InvariantCulture),
            cli_cost_centre = regionLookup[claimPayment.Application.Region.ToString()],
            cli_job = defaultDictionary[nameof(CLI_Invoice.cli_job)],
            cli_activity = tenureLookup[$"{EfinConstants.Lookups.Claim}_{claimPayment.Application.Tenure}"],
        };
    }

    public async Task<CLA_InvoiceAnalysis> CreateClaInvoiceAnalysis(ClaimPaymentRequest claimPayment)
    {
        ArgumentNullException.ThrowIfNull(claimPayment);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Lookups.ClaimDefault);
        var milestoneLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.MilestoneLookup);
        var regionLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.RegionLookup);
        var tenureLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.TenureLookup);
        var partnerTypeLookup = await _lookupCacheService.GetValue(EfinConstants.Lookups.PartnerTypeLookup);

        return new CLA_InvoiceAnalysis()
        {
            cla_sub_ledger = defaultDictionary[nameof(CLA_InvoiceAnalysis.cla_sub_ledger)],
            cla_inv_ref = claimPayment.Claim.InvoiceId,
            cla_batch_ref = string.Empty,
            cla_cfacs_cc = regionLookup[claimPayment.Application.Region.ToString()],
            cla_cfacs_ac = partnerTypeLookup[$"{EfinConstants.Lookups.Claim}_{claimPayment.Application.RevenueIndicator}_{claimPayment.Account.PartnerType}"],
            cla_cfacs_actv = tenureLookup[$"{EfinConstants.Lookups.Claim}_{claimPayment.Application.Tenure}"],
            cla_cfacs_job = defaultDictionary[nameof(CLA_InvoiceAnalysis.cla_cfacs_job)],
            cla_amount = claimPayment.Claim.Amount.ToString(DecimalFormat, CultureInfo.InvariantCulture),
            cla_vat_code = ((int)claimPayment.Application.VatCode).ToString("D2", CultureInfo.InvariantCulture),
            cla_vat_rate = claimPayment.Application.VatRate.ToString(DecimalFormat, CultureInfo.InvariantCulture),
            cla_vat = CalculateVatAmount(claimPayment.Claim.Amount, claimPayment.Application.VatRate).ToString(DecimalFormat, CultureInfo.InvariantCulture),
            cla_description = GetDescription(claimPayment.Claim, claimPayment.Claim.Milestone, claimPayment.Application, milestoneLookup),
            cla_unit_qty = defaultDictionary[nameof(CLA_InvoiceAnalysis.cla_unit_qty)],
            cla_uom = defaultDictionary[nameof(CLA_InvoiceAnalysis.cla_uom)],
            cla_volume = defaultDictionary[nameof(CLA_InvoiceAnalysis.cla_volume)],
        };
    }
}
