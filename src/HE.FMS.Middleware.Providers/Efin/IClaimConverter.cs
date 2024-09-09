using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Claims.Efin;

namespace HE.FMS.Middleware.Providers.Efin;
public interface IClaimConverter
{
    ClaimItemSet Convert(IEnumerable<ClaimPaymentRequest> paymentRequests);
}
