using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Tests.Factories;
using HE.FMS.Middleware.Contract.Claims.Efin;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Efin;

public class ClaimConverterTests
{
    private readonly ClaimConverter _claimConverter = new();

    [Fact]
    public void Convert_ShouldReturnExpectedClaimItem()
    {
        // Arrange
        var claimPaymentRequest = PaymentRequestFactory.CreateRandomClaimPaymentRequest();

        var expectedClaimItem = new ClaimItem
        {
            CliInvoice = _claimConverter.CreateCliInvoice(claimPaymentRequest),
            ClaInvoiceAnalysis = _claimConverter.CreateClaInvoiceAnalysis(claimPaymentRequest),
        };

        // Act
        var result = _claimConverter.CreateItems(claimPaymentRequest);

        // Assert
        Assert.Equivalent(expectedClaimItem.CliInvoice, result.CliInvoice);
        Assert.Equivalent(expectedClaimItem.ClaInvoiceAnalysis, result.ClaInvoiceAnalysis);
    }
}
