using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Contract.Reclaims.Efin;

namespace HE.FMS.Middleware.Providers.Efin;
public interface IReclaimConverter
{
    Task<ReclaimItem> Convert(ReclaimPaymentRequest reclaimPaymentRequest);
}
