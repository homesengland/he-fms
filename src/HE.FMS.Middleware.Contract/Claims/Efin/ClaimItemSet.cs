using HE.FMS.Middleware.Contract.Common;

namespace HE.FMS.Middleware.Contract.Claims.Efin;
public class ClaimItemSet : IItemSet
{
    public CLCLB_Batch CLCLB_Batch { get; set; }

    public IList<CLI_Invoice> CLI_Invoices { get; set; } = new List<CLI_Invoice>();

    public IList<CLA_InvoiceAnalysis> CLA_InvoiceAnalyses { get; set; } = new List<CLA_InvoiceAnalysis>();

    public string IdempotencyKey { get; set; }
}
