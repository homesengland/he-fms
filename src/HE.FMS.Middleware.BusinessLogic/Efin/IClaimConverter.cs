using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Claims.Efin;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public interface IClaimConverter
{
    Task<ClaimItem> CreateItems(ClaimPaymentRequest claimPaymentRequest);

    Task<CLCLB_Batch> CreateBatch(IEnumerable<ClaimItem> claims, string batchRef);
}
