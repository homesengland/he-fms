using System.Globalization;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Providers.Common;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public abstract class PaymentConverter
{
    protected const string DecimalFormat = "F";
    protected const string DateFormat = "d-MMM-yy";

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

    protected string GetInvoiceRef(
        ClaimDetailsBase claimDetails,
        ClaimApplicationDetails applicationDetails,
        Dictionary<string, string> milestoneShortLookup)
    {
        return string.Format(
            CultureInfo.InvariantCulture,
            "{0}{1}",
            applicationDetails.AllocationId,
            milestoneShortLookup[claimDetails.Milestone.ToString()]);
    }

    protected decimal CalculateVatAmount(decimal netAmount, decimal vatRate)
    {
        return netAmount * (vatRate / 100);
    }

    protected decimal CalculateGrossAmount(decimal netAmount, decimal vatRate)
    {
        return netAmount + CalculateVatAmount(netAmount, vatRate);
    }

    protected int GetAccountingYear(DateTime dateTime)
    {
        return dateTime.Month is >= 1 and <= 3 ? dateTime.Year : dateTime.Year + 1;
    }

    protected int GetAccountingPeriod(DateTime dateTime)
    {
        return dateTime.Month is >= 1 and <= 3 ? dateTime.Month + 9 : dateTime.Month - 3;
    }
}
