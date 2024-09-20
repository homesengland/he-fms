using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Tests.Factories;
using HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
using HE.FMS.Middleware.Contract.Reclaims.Efin;
using HE.FMS.Middleware.Providers.Common;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Efin;
public class ReclaimConverterTests
{
    private readonly ReclaimConverter _reclaimConverter = new(new FakeDateTimeProvider(), new FakeEfinLookupService());

    [Fact]
    public async Task Convert_ShouldReturnExpectedReclaimItem()
    {
        // Arrange
        var reclaimPaymentRequest = PaymentRequestFactory.CreateRandomReclaimPaymentRequest();

        var expectedReclaimItem = new ReclaimItem
        {
            CliIwIlt = await _reclaimConverter.CreateCliIwIlt(reclaimPaymentRequest),
            CliIwIna = await _reclaimConverter.CreateCliIwIna(reclaimPaymentRequest),
            CliIwInl = await _reclaimConverter.CreateCliIwInl(reclaimPaymentRequest),
            CliIwInv = await _reclaimConverter.CreateCliIwInv(reclaimPaymentRequest),
            CliIwItl = await _reclaimConverter.CreateCliIwItl(reclaimPaymentRequest),
        };

        // Act
        var result = await _reclaimConverter.CreateItems(reclaimPaymentRequest);

        // Assert
        Assert.Equivalent(expectedReclaimItem.CliIwIlt, result.CliIwIlt);
        Assert.Equivalent(expectedReclaimItem.CliIwIna, result.CliIwIna);
        Assert.Equivalent(expectedReclaimItem.CliIwInl, result.CliIwInl);
        Assert.Equivalent(expectedReclaimItem.CliIwInv, result.CliIwInv);
        Assert.Equivalent(expectedReclaimItem.CliIwItl, result.CliIwItl);
    }
}
