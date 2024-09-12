using HE.FMS.Middleware.Contract.Reclaims.Efin;
using HE.FMS.Middleware.Providers.Efin;
using HE.FMS.Middleware.Providers.Tests.Factories;
using Xunit;

namespace HE.FMS.Middleware.Providers.Tests.Efin;
public class ReclaimConverterTests
{
    private readonly ReclaimConverter _reclaimConverter = new();

    [Fact]
    public void Convert_ShouldReturnExpectedReclaimItem()
    {
        // Arrange
        var reclaimPaymentRequest = PaymentRequestFactory.CreateRandomReclaimPaymentRequest();

        var expectedReclaimItem = new ReclaimItem
        {
            CliIwIlt = CLI_IW_ILT.Create(reclaimPaymentRequest),
            CliIwIna = CLI_IW_INA.Create(reclaimPaymentRequest),
            CliIwInl = CLI_IW_INL.Create(reclaimPaymentRequest),
            CliIwInv = CLI_IW_INV.Create(reclaimPaymentRequest),
            CliIwItl = CLI_IW_ITL.Create(reclaimPaymentRequest),
        };

        // Act
        var result = _reclaimConverter.Convert(reclaimPaymentRequest);

        // Assert
        Assert.Equivalent(expectedReclaimItem.CliIwIlt, result.CliIwIlt);
        Assert.Equivalent(expectedReclaimItem.CliIwIna, result.CliIwIna);
        Assert.Equivalent(expectedReclaimItem.CliIwInl, result.CliIwInl);
        Assert.Equivalent(expectedReclaimItem.CliIwInv, result.CliIwInv);
        Assert.Equivalent(expectedReclaimItem.CliIwItl, result.CliIwItl);
    }
}
