using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Claims.Efin;
using HE.FMS.Middleware.Providers.Config;
using HE.FMS.Middleware.Providers.CosmosDb;
using static HE.FMS.Middleware.Common.Constants;

namespace HE.FMS.Middleware.Providers.Efin;
public class ClaimConverter : IClaimConverter
{
    private readonly IConfigurationClient _configurationClient;

    public ClaimConverter(IConfigurationClient configurationClient)
    {
        _configurationClient = configurationClient;
    }

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

    public async Task<ClaimItem> Convert(ClaimPaymentRequest claimPaymentRequest)
    {
        string invoiceNumber;
        try
        {
            invoiceNumber = await _configurationClient.GetNextIndex(IndexConfiguration.Claim.InvoiceIndex, CosmosDbItemType.Claim);
        }
        catch (MissingFieldException)
        {
            await _configurationClient.CreateItem(IndexConfiguration.Claim.InvoiceIndex, CosmosDbItemType.Claim, IndexConfiguration.Claim.InvoiceIndexPrefix, IndexConfiguration.Claim.InvoiceIndexLength);
            invoiceNumber = await _configurationClient.GetNextIndex(IndexConfiguration.Claim.InvoiceIndex, CosmosDbItemType.Claim);
        }

        return new ClaimItem
        {
            CliInvoice = CLI_Invoice.Create(claimPaymentRequest, invoiceNumber),
            ClaInvoiceAnalysis = CLA_InvoiceAnalysis.Create(claimPaymentRequest, invoiceNumber),
        };
    }
}
