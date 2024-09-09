using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Claims.Efin;
using HE.FMS.Middleware.Providers.Config;

namespace HE.FMS.Middleware.Providers.Efin;
public class ClaimConverter : IClaimConverter
{
    public ClaimItemSet Convert(IEnumerable<ClaimPaymentRequest> paymentRequests)
    {
        var batch = CLCLB_Batch.Create(paymentRequests);
        var invoices = paymentRequests.Select(x => CLI_Invoice.Create(batch, x));
        var invoiceAnalysises = paymentRequests.Select(x => CLA_InvoiceAnalysis.Create(batch, x));

        return new ClaimItemSet()
        {
            CLCLB_Batch = batch,
            CLI_Invoices = invoices,
            CLA_InvoiceAnalyses = invoiceAnalysises,
        };
    }
}
