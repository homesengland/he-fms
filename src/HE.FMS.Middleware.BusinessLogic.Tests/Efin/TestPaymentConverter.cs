using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.Contract.Claims;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Efin;

public class TestPaymentConverter : PaymentConverter
{
    public string PublicGetDescription(
        ClaimDetailsBase claimDetails,
        ClaimApplicationDetails applicationDetails,
        Dictionary<string, string> milestoneLookup)
    {
        return GetDescription(claimDetails, applicationDetails, milestoneLookup);
    }

    public string PublicGetInvoiceRef(
        ClaimDetailsBase claimDetails,
        ClaimApplicationDetails applicationDetails,
        Dictionary<string, string> milestoneShortLookup)
    {
        return GetInvoiceRef(claimDetails, applicationDetails, milestoneShortLookup);
    }

    public decimal PublicCalculateVatAmount(decimal netAmount, decimal vatRate)
    {
        return CalculateVatAmount(netAmount, vatRate);
    }

    public decimal PublicCalculateGrossAmount(decimal netAmount, decimal vatRate)
    {
        return CalculateGrossAmount(netAmount, vatRate);
    }

    public int PublicGetAccountingYear(DateTime dateTime)
    {
        return GetAccountingYear(dateTime);
    }

    public int PublicGetAccountingPeriod(DateTime dateTime)
    {
        return GetAccountingPeriod(dateTime);
    }
}
