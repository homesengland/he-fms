using HE.FMS.Middleware.Contract.Common;

namespace HE.FMS.Middleware.Contract.Claims.Efin;
public class ClaimItemSet : IItemSet
{
    public CLCLB_Batch CLCLB_Batch { get; set; }

    public IEnumerable<CLI_Invoice> CLI_Invoices { get; set; }

    public IEnumerable<CLA_InvoiceAnalysis> CLA_InvoiceAnalyses { get; set; }

    public string IdempotencyKey { get; set; }
}
