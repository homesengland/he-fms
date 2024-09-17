using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Claims.Efin;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public interface IClaimConverter
{
    ClaimItem CreateItems(ClaimPaymentRequest claimPaymentRequest);

    CLCLB_Batch CreateBatch(IEnumerable<ClaimItem> claims, string batchRef);
}
