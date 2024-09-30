using System.Globalization;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Reclaims;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public abstract class PaymentConverter
{
    protected string GetDescription(
        ClaimDetailsBase claimDetails,
        ClaimApplicationDetails applicationDetails,
        Dictionary<string, string> milestoneLookup)
    {
        return string.Format(
            CultureInfo.InvariantCulture,
            "{0}{1} {2}",
            milestoneLookup[claimDetails.Milestone.ToString()],
            applicationDetails.ApplicationId,
            applicationDetails.SchemaName.Truncate(19));
    }

    protected decimal CalculateVatAmount(decimal netAmount, decimal vatRate)
    {
        return netAmount * vatRate;
    }

    protected decimal CalculateGrossAmount(decimal netAmount, decimal vatRate)
    {
        return netAmount + CalculateVatAmount(netAmount, vatRate);
    }
}
