using HE.FMS.Middleware.Contract.Common;
using Microsoft.Extensions.Configuration;

// ReSharper disable InconsistentNaming
namespace HE.FMS.Middleware.Contract.Claims.Efin;
public class ClaimItemSet : IItemSet
{
    public CLCLB_Batch CLCLB_Batch { get; set; }

    public IList<CLI_Invoice> CLI_Invoices { get; set; } = [];

    public IList<CLA_InvoiceAnalysis> CLA_InvoiceAnalyses { get; set; } = [];

    public string IdempotencyKey { get; set; }

    public string BatchNumber { get; set; }
}
