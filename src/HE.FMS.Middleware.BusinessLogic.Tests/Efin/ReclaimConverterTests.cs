using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Tests.Factories;
using HE.FMS.Middleware.Contract.Reclaims.Efin;
using HE.FMS.Middleware.Providers.Common;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Efin;
public class ReclaimConverterTests
{
    private readonly ReclaimConverter _reclaimConverter = new(new FakeDateTimeProvider());

    [Fact]
    public void Convert_ShouldReturnExpectedReclaimItem()
    {
        // Arrange
        var reclaimPaymentRequest = PaymentRequestFactory.CreateRandomReclaimPaymentRequest();

        var expectedReclaimItem = new ReclaimItem
        {
            CliIwIlt = _reclaimConverter.CreateCliIwIlt(reclaimPaymentRequest),
            CliIwIna = _reclaimConverter.CreateCliIwIna(reclaimPaymentRequest),
            CliIwInl = _reclaimConverter.CreateCliIwInl(reclaimPaymentRequest),
            CliIwInv = _reclaimConverter.CreateCliIwInv(reclaimPaymentRequest),
            CliIwItl = _reclaimConverter.CreateCliIwItl(reclaimPaymentRequest),
        };

        // Act
        var result = _reclaimConverter.CreateItems(reclaimPaymentRequest);

        // Assert
        Assert.Equivalent(expectedReclaimItem.CliIwIlt, result.CliIwIlt);
        Assert.Equivalent(expectedReclaimItem.CliIwIna, result.CliIwIna);
        Assert.Equivalent(expectedReclaimItem.CliIwInl, result.CliIwInl);
        Assert.Equivalent(expectedReclaimItem.CliIwInv, result.CliIwInv);
        Assert.Equivalent(expectedReclaimItem.CliIwItl, result.CliIwItl);
    }
}
