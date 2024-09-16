using System.Globalization;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Reclaims.Efin;
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
public class CLI_IW_ITL
{
    public const string FileName = "cpiw_hf";

    [EfinFileRowIndex(1, 3)]
    public string cliwx_sub_ledger_id { get; set; }

    [EfinFileRowIndex(4, 10)]
    public string cliwx_batch_ref { get; set; }

    [EfinFileRowIndex(11, 18)]
    public string cliwx_inv_ref { get; set; }

    [EfinFileRowIndex(19, 22)]
    public string cliwx_line_no { get; set; }

    [EfinFileRowIndex(23, 23)]
    public string cliwx_header_footer { get; set; }

    [EfinFileRowIndex(24, 98)]
    public string cliwx_text { get; set; }

    public static CLI_IW_ITL Create(ReclaimPaymentRequest reclaimPayment)
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
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
