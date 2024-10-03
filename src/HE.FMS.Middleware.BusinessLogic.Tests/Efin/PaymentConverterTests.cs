using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Enums;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Efin;

public class PaymentConverterTests
{
    [Fact]
    public void GetDescription_ShouldReturnFormattedDescription()
    {
        // Arrange  
        var converter = new TestPaymentConverter();
        var claimDetails = new ClaimDetailsBase { Milestone = Milestone.Acquisition };
        var applicationDetails = new ClaimApplicationDetails
        {
            ApplicationId = "12345",
            SchemaName = "LongSchemaNameThatNeedsTruncation",
        };
        var milestoneLookup = new Dictionary<string, string> { { "Acquisition", "ACQ" } };

        // Act  
        var description = converter.PublicGetDescription(claimDetails, applicationDetails, milestoneLookup);

        // Assert  
        Assert.Equal("ACQ12345 LongSchemaNameThatN", description);
    }

    [Fact]
    public void CalculateVatAmount_ShouldReturnCorrectVatAmount()
    {
        // Arrange  
        var converter = new TestPaymentConverter();
        var netAmount = 100m;
        var vatRate = 20m;

        // Act  
        var vatAmount = converter.PublicCalculateVatAmount(netAmount, vatRate);

        // Assert  
        Assert.Equal(20m, vatAmount);
    }

    [Fact]
    public void CalculateGrossAmount_ShouldReturnCorrectGrossAmount()
    {
        // Arrange  
        var converter = new TestPaymentConverter();
        var netAmount = 100m;
        var vatRate = 20m;

        // Act  
        var grossAmount = converter.PublicCalculateGrossAmount(netAmount, vatRate);

        // Assert  
        Assert.Equal(120m, grossAmount);
    }
}
