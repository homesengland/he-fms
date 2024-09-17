using System.Globalization;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Claims.Efin;
using HE.FMS.Middleware.Contract.Constants;
using HE.FMS.Middleware.Providers.Common;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public class ClaimConverter : IClaimConverter
{
    public ClaimItem CreateItems(ClaimPaymentRequest claimPaymentRequest)
    {
        return new ClaimItem
        {
            CliInvoice = CreateCliInvoice(claimPaymentRequest),
            ClaInvoiceAnalysis = CreateClaInvoiceAnalysis(claimPaymentRequest),
        };
    }

    public CLCLB_Batch CreateBatch(IEnumerable<ClaimItem> claims, string batchRef)
    {
        ArgumentNullException.ThrowIfNull(claims);

        var culture = new CultureInfo("en-GB");
        return new CLCLB_Batch()
        {
            clb_sub_ledger = EfinConstants.Default.Claim.SubLedger,

            clb_batch_ref = batchRef,
            clb_year = (DateTime.UtcNow.Month is >= 1 and <= 3 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year).ToString(CultureInfo.InvariantCulture),
            clb_period = DateTime.UtcNow.Month.ToString(CultureInfo.InvariantCulture),
            clb_net_amount = claims.Sum(x => decimal.Parse(x.CliInvoice.cli_net_amount, NumberStyles.Any, culture)).ToString("F", CultureInfo.InvariantCulture),
            clb_vat_amount = EfinConstants.Default.Claim.Amount,
            clb_no_invoices = claims.Count().ToString(CultureInfo.InvariantCulture),
            clb_quantity = claims.Count().ToString(CultureInfo.InvariantCulture),
            clb_user = EfinConstants.Default.Claim.User,
            clb_grouping = EfinConstants.Default.Claim.Grouping,
            clb_entry_date = DateTime.UtcNow.ToString("d-MMM-yy", CultureInfo.InvariantCulture),
        };
    }

    public CLI_Invoice CreateCliInvoice(ClaimPaymentRequest claimPayment)
    {
        return new CLI_Invoice()
        {
            cli_sub_ledger = EfinConstants.Default.Claim.SubLedger,
            cli_inv_ref = claimPayment.Application.AllocationId,
            cli_batch_ref = string.Empty,
            cli_cfacs_customer = claimPayment.Claim.Id,
            cli_net_amount = claimPayment.Claim.Amount.ToString("F", CultureInfo.InvariantCulture),
            cli_vat = EfinConstants.Default.Claim.Amount,
            cli_gross = claimPayment.Claim.Amount.ToString("F", CultureInfo.InvariantCulture),
            cli_volume = EfinConstants.Default.Claim.Volume,
            cli_uom = EfinConstants.Default.Claim.UOM,
            cli_our_ref_2 = claimPayment.Application.AllocationId,
            cli_their_ref = claimPayment.Application.AllocationId,
            cli_trans_type = EfinConstants.Default.Claim.TransType,
            cli_date = claimPayment.Claim.AuthorisedOn.ToString(CultureInfo.InvariantCulture),
            cli_description = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", claimPayment.Claim.Milestone[..3], claimPayment.Claim.Id, claimPayment.Application.Id),
            cli_terms_code = EfinConstants.Default.Claim.TermsCode,
            cli_due_date = claimPayment.Claim.AuthorisedOn.AddDays(7).ToString(CultureInfo.InvariantCulture),
            cli_cost_centre = claimPayment.Application.EfinRegion,
            cli_job = claimPayment.Application.Id,
            cli_activity = claimPayment.Application.EfinTenure,
        };
    }

    public CLA_InvoiceAnalysis CreateClaInvoiceAnalysis(ClaimPaymentRequest claimPayment)
    {
        return new CLA_InvoiceAnalysis()
        {
            cla_sub_ledger = EfinConstants.Default.Claim.SubLedger,
            cla_inv_ref = claimPayment.Application.AllocationId,
            cla_batch_ref = string.Empty,
            cla_cfacs_cc = claimPayment.Application.EfinRegion,
            cla_cfacs_ac = claimPayment.Organisation.PartnerType,
            cla_cfacs_actv = claimPayment.Application.EfinTenure,
            cla_cfacs_job = claimPayment.Application.Id,
            cla_amount = claimPayment.Claim.Amount.ToString("F", CultureInfo.InvariantCulture),
            cla_vat_code = claimPayment.Application.VatCode,
            cla_vat_rate = claimPayment.Application.VatRate.ToString("F", CultureInfo.InvariantCulture),
            cla_vat = (claimPayment.Claim.Amount * claimPayment.Application.VatRate).ToString("F", CultureInfo.InvariantCulture),
            cla_description = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", claimPayment.Claim.Milestone[..3], claimPayment.Claim.Id, claimPayment.Application.Id),
            cla_unit_qty = EfinConstants.Default.Claim.UnitQuantity,
            cla_uom = EfinConstants.Default.Claim.UOM,
            cla_volume = EfinConstants.Default.Claim.Volume,
        };
    }
}
