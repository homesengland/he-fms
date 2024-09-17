using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Contract.Reclaims.Efin;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public class ReclaimConverter : IReclaimConverter
{
    public ReclaimItem Convert(ReclaimPaymentRequest reclaimPaymentRequest)
    {
        return new ReclaimItem
        {
            CliIwIlt = CLI_IW_ILT.Create(reclaimPaymentRequest),
            CliIwIna = CLI_IW_INA.Create(reclaimPaymentRequest),
            CliIwInl = CLI_IW_INL.Create(reclaimPaymentRequest),
            CliIwInv = CLI_IW_INV.Create(reclaimPaymentRequest),
            CliIwItl = CLI_IW_ITL.Create(reclaimPaymentRequest),
        };
    }
}
