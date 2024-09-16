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
            CliInvoice = CLI_Invoice.Create(claimPaymentRequest),
            ClaInvoiceAnalysis = CLA_InvoiceAnalysis.Create(claimPaymentRequest),
        };

        // Act
        var result = _claimConverter.Convert(claimPaymentRequest);

        // Assert
        Assert.Equivalent(expectedClaimItem.CliInvoice, result.CliInvoice);
        Assert.Equivalent(expectedClaimItem.ClaInvoiceAnalysis, result.ClaInvoiceAnalysis);
    }
}
