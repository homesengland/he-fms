using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Claims.Efin;
using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Efin;
using static HE.FMS.Middleware.Common.Constants;

namespace HE.FMS.Middleware.Providers.Efin;
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
