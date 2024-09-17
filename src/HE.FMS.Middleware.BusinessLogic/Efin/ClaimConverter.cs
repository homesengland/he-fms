using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Claims.Efin;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public class ClaimConverter : IClaimConverter
{
    public ClaimItem Convert(ClaimPaymentRequest claimPaymentRequest)
    {
        return new ClaimItem
        {
            CliInvoice = CLI_Invoice.Create(claimPaymentRequest),
            ClaInvoiceAnalysis = CLA_InvoiceAnalysis.Create(claimPaymentRequest),
        };
    }
}
