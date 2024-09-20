using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Contract.Reclaims.Efin;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public interface IReclaimConverter
{
    Task<ReclaimItem> CreateItems(ReclaimPaymentRequest reclaimPaymentRequest);

    Task<CLI_IW_BAT> CreateBatch(IEnumerable<ReclaimItem> reclaims, string batchRef);
}
