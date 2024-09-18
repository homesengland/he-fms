using System.Globalization;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Claims.Efin;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public class ClaimConverter : IClaimConverter
{
    private readonly IEfinLookupCacheService _lookupCacheService;

    public ClaimConverter(IEfinLookupCacheService lookupCacheService)
    {
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

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Default.ClaimDefault);

        var culture = new CultureInfo("en-GB");
        return new CLCLB_Batch()
        {
            clb_sub_ledger = defaultDictionary[nameof(CLCLB_Batch.clb_sub_ledger)],
            clb_batch_ref = batchRef,
            clb_year = (DateTime.UtcNow.Month is >= 1 and <= 3 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year).ToString(CultureInfo.InvariantCulture),
            clb_period = DateTime.UtcNow.Month.ToString(CultureInfo.InvariantCulture),
            clb_net_amount = claims.Sum(x => decimal.Parse(x.CliInvoice.cli_net_amount, NumberStyles.Any, culture)).ToString("F", CultureInfo.InvariantCulture),
            clb_vat_amount = defaultDictionary[nameof(CLCLB_Batch.clb_vat_amount)],
            clb_no_invoices = claims.Count().ToString(CultureInfo.InvariantCulture),
            clb_quantity = claims.Count().ToString(CultureInfo.InvariantCulture),
            clb_user = defaultDictionary[nameof(CLCLB_Batch.clb_user)],
            clb_grouping = defaultDictionary[nameof(CLCLB_Batch.clb_grouping)],
            clb_entry_date = DateTime.UtcNow.ToString("d-MMM-yy", CultureInfo.InvariantCulture),
        };
    }

    public async Task<CLI_Invoice> CreateCliInvoice(ClaimPaymentRequest claimPayment)
    {
        ArgumentNullException.ThrowIfNull(claimPayment);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Default.ClaimDefault);
        var milestoneLookup = await _lookupCacheService.GetValue(EfinConstants.Default.MilestoneLookup);
        var regionLookup = await _lookupCacheService.GetValue(EfinConstants.Default.RegionLookup);

        return new CLI_Invoice()
        {
            cli_sub_ledger = defaultDictionary[nameof(CLI_Invoice.cli_sub_ledger)],
            cli_inv_ref = claimPayment.Application.AllocationId,
            cli_batch_ref = string.Empty,
            cli_cfacs_customer = claimPayment.Claim.Id,
            cli_net_amount = claimPayment.Claim.Amount.ToString("F", CultureInfo.InvariantCulture),
            cli_vat = defaultDictionary[nameof(CLI_Invoice.cli_vat)],
            cli_gross = claimPayment.Claim.Amount.ToString("F", CultureInfo.InvariantCulture),
            cli_volume = defaultDictionary[nameof(CLI_Invoice.cli_volume)],
            cli_uom = defaultDictionary[nameof(CLI_Invoice.cli_uom)],
            cli_our_ref_2 = claimPayment.Application.AllocationId,
            cli_their_ref = claimPayment.Application.AllocationId,
            cli_trans_type = defaultDictionary[nameof(CLI_Invoice.cli_trans_type)],
            cli_date = claimPayment.Claim.AuthorisedOn.ToString(CultureInfo.InvariantCulture),
            cli_description = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", milestoneLookup[claimPayment.Claim.Milestone], claimPayment.Claim.Id, claimPayment.Application.Id),
            cli_terms_code = defaultDictionary[nameof(CLI_Invoice.cli_terms_code)],
            cli_due_date = claimPayment.Claim.AuthorisedOn.AddDays(7).ToString(CultureInfo.InvariantCulture),
            cli_cost_centre = regionLookup[claimPayment.Application.Region],
            cli_job = claimPayment.Application.Id,
            cli_activity = claimPayment.Application.EfinTenure,
        };
    }

    public async Task<CLA_InvoiceAnalysis> CreateClaInvoiceAnalysis(ClaimPaymentRequest claimPayment)
    {
        ArgumentNullException.ThrowIfNull(claimPayment);

        var defaultDictionary = await _lookupCacheService.GetValue(EfinConstants.Default.ClaimDefault);
        var milestoneLookup = await _lookupCacheService.GetValue(EfinConstants.Default.MilestoneLookup);
        var regionLookup = await _lookupCacheService.GetValue(EfinConstants.Default.RegionLookup);

        return new CLA_InvoiceAnalysis()
        {
            cla_sub_ledger = defaultDictionary[nameof(CLA_InvoiceAnalysis.cla_sub_ledger)],
            cla_inv_ref = claimPayment.Application.AllocationId,
            cla_batch_ref = string.Empty,
            cla_cfacs_cc = regionLookup[claimPayment.Application.Region],
            cla_cfacs_ac = claimPayment.Organisation.PartnerType,
            cla_cfacs_actv = claimPayment.Application.EfinTenure,
            cla_cfacs_job = claimPayment.Application.Id,
            cla_amount = claimPayment.Claim.Amount.ToString("F", CultureInfo.InvariantCulture),
            cla_vat_code = claimPayment.Application.VatCode,
            cla_vat_rate = claimPayment.Application.VatRate.ToString("F", CultureInfo.InvariantCulture),
            cla_vat = (claimPayment.Claim.Amount * claimPayment.Application.VatRate).ToString("F", CultureInfo.InvariantCulture),
            cla_description = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", milestoneLookup[claimPayment.Claim.Milestone], claimPayment.Claim.Id, claimPayment.Application.Id),
            cla_unit_qty = defaultDictionary[nameof(CLA_InvoiceAnalysis.cla_unit_qty)],
            cla_uom = defaultDictionary[nameof(CLA_InvoiceAnalysis.cla_uom)],
            cla_volume = defaultDictionary[nameof(CLA_InvoiceAnalysis.cla_volume)],
        };
    }
}
