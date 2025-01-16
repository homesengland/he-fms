using System.Globalization;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Enums;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public abstract class PaymentConverter
{
    protected const string DecimalFormat = "F";
    protected const string DateFormat = "d-MMM-yy";

    public static string GetDescription(
        ClaimDetailsBase claimDetails,
        Milestone? milestone,
        ClaimApplicationDetails applicationDetails,
        Dictionary<string, string> milestoneLookup)
    {
        if (milestone.HasValue)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0}{1} {2}",
                milestoneLookup[milestone.Value.ToString()],
                applicationDetails.ApplicationId,
                applicationDetails.SchemaName.RemoveSpecialCharacters().Truncate(19));
        }

        return string.Format(
            CultureInfo.InvariantCulture,
            "{0}{1}{2}",
            applicationDetails.SchemaName.RemoveSpecialCharacters().Truncate(14),
            claimDetails.InvoiceId,
            applicationDetails.AllocationId.Truncate(7));
    }

    public static decimal CalculateVatAmount(decimal netAmount, decimal vatRate)
    {
        return netAmount * (vatRate / 100);
    }

    public static decimal CalculateGrossAmount(decimal netAmount, decimal vatRate)
    {
        return netAmount + CalculateVatAmount(netAmount, vatRate);
    }

    public static int GetAccountingYear(DateTime dateTime)
    {
        return dateTime.Month is >= 1 and <= 3 ? dateTime.Year : dateTime.Year + 1;
    }

    public static int GetAccountingPeriod(DateTime dateTime)
    {
        return dateTime.Month is >= 1 and <= 3 ? dateTime.Month + 9 : dateTime.Month - 3;
    }
}
