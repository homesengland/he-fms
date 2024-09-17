using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Contract.Reclaims.Efin;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public interface IReclaimConverter
{
    ReclaimItem CreateItems(ReclaimPaymentRequest reclaimPaymentRequest);

    CLI_IW_BAT CreateBatch(IEnumerable<ReclaimItem> reclaims, string batchRef);
}
