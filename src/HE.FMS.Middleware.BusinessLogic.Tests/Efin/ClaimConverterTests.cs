using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Tests.Factories;
using HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
using HE.FMS.Middleware.Contract.Claims.Efin;
using HE.FMS.Middleware.Providers.Common;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Efin;

public class ClaimConverterTests
{
    private readonly ClaimConverter _claimConverter = new(new FakeDateTimeProvider(), new FakeEfinLookupService());

    [Fact]
    public async Task Convert_ShouldReturnExpectedClaimItem()
    {
        // Arrange
        var claimPaymentRequest = PaymentRequestFactory.CreateRandomClaimPaymentRequest();

        var expectedClaimItem = new ClaimItem
        {
            CliInvoice = await _claimConverter.CreateCliInvoice(claimPaymentRequest),
            ClaInvoiceAnalysis = await _claimConverter.CreateClaInvoiceAnalysis(claimPaymentRequest),
        };

        // Act
        var result = await _claimConverter.CreateItems(claimPaymentRequest);

        // Assert
        Assert.Equivalent(expectedClaimItem.CliInvoice, result.CliInvoice);
        Assert.Equivalent(expectedClaimItem.ClaInvoiceAnalysis, result.ClaInvoiceAnalysis);
    }
}
