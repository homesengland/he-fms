using Bogus;
using Bogus.Extensions;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Constants;
using HE.FMS.Middleware.Contract.Reclaims;

namespace HE.FMS.Middleware.Providers.Tests.Factories;
public static class PaymentRequestFactory
{
    public static ClaimPaymentRequest CreateRandomClaimPaymentRequest()
    {
        var randomizer = new Randomizer();

        return new ClaimPaymentRequest
        {
            Claim = new ClaimDetailsBase
            {
                Amount = randomizer.Decimal(),
                Id = randomizer.String2(10),
                Milestone = EfinConstants.Milestone.Acquisition,
                AuthorisedOn = DateTimeOffset.UtcNow,
                AuthorisedBy = randomizer.String2(10),
            },
            Application = new ClaimApplicationDetails
            {
                AllocationId = randomizer.String2(10),
                Id = randomizer.String2(10),
                Name = randomizer.String2(10),
                Region = EfinConstants.Region.North,
                RevenueIndicator = EfinConstants.RevenueIndicator.Capital,
                Tenure = nameof(EfinConstants.Tenure.SocialRent),
                VatCode = "05",
                VatRate = 23,
            },
            Organisation = new ClaimOrganisationDetails
            {
                Name = randomizer.String2(10),
                PartnerType = nameof(EfinConstants.CapitalClaimPartnerType.Bank),
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
                Milestone = EfinConstants.Milestone.Acquisition,
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
                Region = EfinConstants.Region.North,
                RevenueIndicator = EfinConstants.RevenueIndicator.Capital,
                Tenure = nameof(EfinConstants.Tenure.SocialRent),
                VatCode = "05",
                VatRate = 23,
            },
            Organisation = new ClaimOrganisationDetails
            {
                Name = randomizer.String2(10),
                PartnerType = nameof(EfinConstants.CapitalClaimPartnerType.Bank),
            },
        };
    }
}