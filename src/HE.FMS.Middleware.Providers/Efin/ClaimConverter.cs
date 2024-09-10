using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Claims.Efin;
using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Efin;
using static HE.FMS.Middleware.Common.Constants;

namespace HE.FMS.Middleware.Providers.Efin;
public class ClaimConverter : IClaimConverter
{
    private readonly IEfinCosmosConfigClient _configurationClient;

    public ClaimConverter(IEfinCosmosConfigClient configurationClient)
    {
        _configurationClient = configurationClient;
    }

    public async Task<ClaimItem> Convert(ClaimPaymentRequest claimPaymentRequest)
    {
        string invoiceNumber;
        try
        {
            invoiceNumber = await _configurationClient.GetNextIndex(IndexConfiguration.Claim.InvoiceIndex, CosmosDbItemType.Claim);
        }
        catch (MissingConfigurationException)
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
