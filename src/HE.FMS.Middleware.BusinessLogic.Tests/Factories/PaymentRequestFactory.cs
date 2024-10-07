using Bogus;
using Bogus.Extensions;
using HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Enums;
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
                Milestone = randomizer.Enum<Milestone>(),
                ApprovedOn = DateTimeProvider.UtcNow,
                ApprovedBy = randomizer.String2(10),
            },
            Application = new ClaimApplicationDetails
            {
                AllocationId = randomizer.String2(10),
                ApplicationId = randomizer.String2(10),
                SchemaName = randomizer.String2(27),
                Region = randomizer.Enum<Region>(),
                RevenueIndicator = randomizer.Enum<RevenueIndicator>(),
                Tenure = randomizer.Enum<Tenure>(),
                VatCode = randomizer.Enum<VatCode>(),
                VatRate = 23,
            },
            Account = new ClaimAccountDetails
            {
                ProviderId = randomizer.String2(10),
                PartnerType = randomizer.Enum<PartnerType>(),
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
                Milestone = randomizer.Enum<Milestone>(),
                InterestAmount = randomizer.Decimal2(),
                TotalAmount = randomizer.Decimal2(),
            },
            Application = new ClaimApplicationDetails
            {
                AllocationId = randomizer.String2(10),
                ApplicationId = randomizer.String2(10),
                SchemaName = randomizer.String2(27),
                Region = randomizer.Enum<Region>(),
                RevenueIndicator = randomizer.Enum<RevenueIndicator>(),
                Tenure = randomizer.Enum<Tenure>(),
                VatCode = randomizer.Enum<VatCode>(),
                VatRate = 23,
            },
            Account = new ClaimAccountDetails
            {
                ProviderId = randomizer.String2(10),
                PartnerType = randomizer.Enum<PartnerType>(),
            },
        };
    }
}
