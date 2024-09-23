using Bogus;
using Bogus.Extensions;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Providers.Common;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Factories;
public static class PaymentRequestFactory
{
    private static readonly FakeDateTimeProvider DateTimeProvider = new();

    public static ClaimPaymentRequest CreateRandomClaimPaymentRequest()
    {
        var randomizer = new Randomizer();

        return new ClaimPaymentRequest
        {
            Claim = new ClaimDetails
            {
                Amount = randomizer.Decimal(),
                Id = randomizer.String2(10),
                Milestone = "Acquisition",
                ApprovedOn = DateTimeProvider.UtcNow,
                ApprovedBy = randomizer.String2(10),
            },
            Application = new ClaimApplicationDetails
            {
                AllocationId = randomizer.String2(10),
                ApplicationId = randomizer.String2(10),
                SchemaName = randomizer.String2(10),
                Region = "North",
                RevenueIndicator = "Capital",
                Tenure = "SocialRent",
                VatCode = "05",
                VatRate = 23,
            },
            Account = new ClaimAccountDetails
            {
                Name = randomizer.String2(10),
                PartnerType = "Bank",
            },
        };
    }

    public static ReclaimPaymentRequest CreateRandomReclaimPaymentRequest()
    {
        var randomizer = new Randomizer();

        return new ReclaimPaymentRequest
        {
            Reclaim = new ReclaimDetails
            {
                Amount = randomizer.Decimal2(),
                Id = randomizer.String2(10),
                Milestone = "Acquisition",
                InterestAmount = randomizer.Decimal2(),
                TotalAmount = randomizer.Decimal2(),
            },
            Application = new ClaimApplicationDetails
            {
                AllocationId = randomizer.String2(10),
                ApplicationId = randomizer.String2(10),
                SchemaName = randomizer.String2(10),
                Region = "North",
                RevenueIndicator = "Capital",
                Tenure = "SocialRent",
                VatCode = "05",
                VatRate = 23,
            },
            Account = new ClaimAccountDetails
            {
                Name = randomizer.String2(10),
                PartnerType = "Bank",
            },
        };
    }
}
