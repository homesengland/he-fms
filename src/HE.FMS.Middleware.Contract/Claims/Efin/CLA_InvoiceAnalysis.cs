using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HE.FMS.Middleware.Contract.Constants;

#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
namespace HE.FMS.Middleware.Contract.Claims.Efin;
public class CLA_InvoiceAnalysis
{
    public string cla_sub_ledger { get; set; }

    public string cla_inv_ref { get; set; }

    public string cla_batch_ref { get; set; }

    public string cla_cfacs_cc { get; set; }

    public string cla_cfacs_ac { get; set; }

    public string cla_cfacs_actv { get; set; }

    public string cla_cfacs_job { get; set; }

    public string cla_amount { get; set; }

    public string cla_vat_code { get; set; }

    public string cla_vat_rate { get; set; }

    public string cla_vat { get; set; }

    public string cla_description { get; set; }

    public string cla_unit_qty { get; set; }

    public string cla_uom { get; set; }

    public string cla_volume { get; set; }

    public static CLA_InvoiceAnalysis Create(CLCLB_Batch batch, ClaimPaymentRequest claimPayment)
    {
        return new CLA_InvoiceAnalysis()
        {
            cla_sub_ledger = EfinConstants.Default.Claim.SubLedger,

            // cla_inv_ref = <unique_value>
            cla_batch_ref = batch.clb_batch_ref,
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
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase