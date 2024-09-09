using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Contract.Reclaims.Efin;
using HE.FMS.Middleware.Providers.Config;
using HE.FMS.Middleware.Providers.CosmosDb;
using static HE.FMS.Middleware.Common.Constants;

namespace HE.FMS.Middleware.Providers.Efin;
public class ReclaimConverter : IReclaimConverter
{
    private readonly IConfigurationClient _configurationClient;

    public ReclaimConverter(IConfigurationClient configurationClient)
    {
        _configurationClient = configurationClient;
    }

    public ReclaimItemSet Convert(IEnumerable<ReclaimPaymentRequest> paymentRequests)
    {
        var batch = CLI_IW_BAT.Create(paymentRequests);
        var iltes = paymentRequests.Select(x => CLI_IW_ILT.Create(batch, x));
        var inaes = paymentRequests.Select(x => CLI_IW_INA.Create(batch, x));
        var inles = paymentRequests.Select(x => CLI_IW_INL.Create(batch, x));
        var inves = paymentRequests.Select(x => CLI_IW_INV.Create(batch, x));
        var itles = paymentRequests.Select(x => CLI_IW_ITL.Create(batch, x));

        return new ReclaimItemSet()
        {
            CLI_IW_BAT = batch,
            CLI_IW_ILTes = iltes,
            CLI_IW_INAes = inaes,
            CLI_IW_INLes = inles,
            CLI_IW_INVes = inves,
            CLI_IW_ITLes = itles,
        };
    }

    public async Task<ReclaimItem> Convert(ReclaimPaymentRequest reclaimPaymentRequest)
    {
        string invoiceNumber;
        try
        {
            invoiceNumber = await _configurationClient.GetNextIndex(IndexConfiguration.Reclaim.InvoiceIndex, CosmosDbItemType.Claim);
        }
        catch (MissingFieldException)
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
