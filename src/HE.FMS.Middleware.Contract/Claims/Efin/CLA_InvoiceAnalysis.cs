using System.Globalization;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Constants;

#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
namespace HE.FMS.Middleware.Contract.Claims.Efin;
[EfinFileRowSize(256)]
public class CLA_InvoiceAnalysis
{
    [EfinFileRowIndex(1, 3)]
    public string cla_sub_ledger { get; set; }

    [EfinFileRowIndex(5, 12)]
    public string cla_inv_ref { get; set; }

    [EfinFileRowIndex(14, 20)]
    public string cla_batch_ref { get; set; }

    [EfinFileRowIndex(22, 27)]
    public string cla_cfacs_cc { get; set; }

    [EfinFileRowIndex(29, 36)]
    public string cla_cfacs_ac { get; set; }

    [EfinFileRowIndex(38, 43)]
    public string cla_cfacs_actv { get; set; }

    [EfinFileRowIndex(45, 52)]
    public string cla_cfacs_job { get; set; }

    [EfinFileRowIndex(56, 72)]
    public string cla_amount { get; set; }

    [EfinFileRowIndex(74, 75)]
    public string cla_vat_code { get; set; }

    [EfinFileRowIndex(77, 83)]
    public string cla_vat_rate { get; set; }

    [EfinFileRowIndex(85, 101)]
    public string cla_vat { get; set; }

    [EfinFileRowIndex(103, 132)]
    public string cla_description { get; set; }

    [EfinFileRowIndex(152, 168)]
    public string cla_unit_qty { get; set; }

    [EfinFileRowIndex(170, 172)]
    public string cla_uom { get; set; }

    [EfinFileRowIndex(240, 256)]
    public string cla_volume { get; set; }

    public static CLA_InvoiceAnalysis Create(ClaimPaymentRequest claimPayment)
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
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
