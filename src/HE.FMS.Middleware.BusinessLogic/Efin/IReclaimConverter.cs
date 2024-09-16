using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Contract.Reclaims.Efin;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public interface IReclaimConverter
{
    ReclaimItem Convert(ReclaimPaymentRequest reclaimPaymentRequest);
}
