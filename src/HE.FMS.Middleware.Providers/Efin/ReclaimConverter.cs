using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Contract.Reclaims.Efin;
using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Efin;
using static HE.FMS.Middleware.Common.Constants;

namespace HE.FMS.Middleware.Providers.Efin;
public class ReclaimConverter : IReclaimConverter
{
    private readonly IEfinCosmosConfigClient _configurationClient;

    public ReclaimConverter(IEfinCosmosConfigClient configurationClient)
    {
        _configurationClient = configurationClient;
    }

    public async Task<ReclaimItem> Convert(ReclaimPaymentRequest reclaimPaymentRequest)
    {
        string invoiceNumber;
        try
        {
            invoiceNumber = await _configurationClient.GetNextIndex(IndexConfiguration.Reclaim.InvoiceIndex, CosmosDbItemType.Claim);
        }
        catch (MissingConfigurationException)
        {
            await _configurationClient.CreateItem(IndexConfiguration.Reclaim.InvoiceIndex, CosmosDbItemType.Claim, IndexConfiguration.Reclaim.InvoiceIndexPrefix, IndexConfiguration.Reclaim.InvoiceIndexLength);
            invoiceNumber = await _configurationClient.GetNextIndex(IndexConfiguration.Reclaim.InvoiceIndex, CosmosDbItemType.Claim);
        }

        return new ReclaimItem
        {
            CliIwIlt = CLI_IW_ILT.Create(reclaimPaymentRequest, invoiceNumber),
            CliIwIna = CLI_IW_INA.Create(reclaimPaymentRequest, invoiceNumber),
            CliIwInl = CLI_IW_INL.Create(reclaimPaymentRequest, invoiceNumber),
            CliIwInv = CLI_IW_INV.Create(reclaimPaymentRequest, invoiceNumber),
            CliIwItl = CLI_IW_ITL.Create(reclaimPaymentRequest, invoiceNumber),
        };
    }
}
