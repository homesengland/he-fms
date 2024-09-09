using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Contract.Reclaims.Efin;

namespace HE.FMS.Middleware.Providers.Efin;
public class ReclaimConverter : IReclaimConverter
{
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
}
