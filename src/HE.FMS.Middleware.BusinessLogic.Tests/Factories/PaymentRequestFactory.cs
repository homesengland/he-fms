using Bogus;
using Bogus.Extensions;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Reclaims;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Factories;
public static class PaymentRequestFactory
{
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
                AuthorisedOn = DateTimeOffset.UtcNow,
                AuthorisedBy = randomizer.String2(10),
            },
            Application = new ClaimApplicationDetails
            {
                AllocationId = randomizer.String2(10),
                Id = randomizer.String2(10),
                Name = randomizer.String2(10),
                Region = "North",
                RevenueIndicator = "Capital",
                Tenure = "SocialRent",
                VatCode = "05",
                VatRate = 23,
            },
            Organisation = new ClaimOrganisationDetails
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
                AuthorisedOn = DateTimeOffset.UtcNow,
                AuthorisedBy = randomizer.String2(10),
                InterestAmount = randomizer.Decimal2(),
                TotalAmount = randomizer.Decimal2(),
            },
            Application = new ClaimApplicationDetails
            {
                AllocationId = randomizer.String2(10),
                Id = randomizer.String2(10),
                Name = randomizer.String2(10),
                Region = "North",
                RevenueIndicator = "Capital",
                Tenure = "SocialRent",
                VatCode = "05",
                VatRate = 23,
            },
            Organisation = new ClaimOrganisationDetails
            {
                Name = randomizer.String2(10),
                PartnerType = "Bank",
            },
        };
    }
}
