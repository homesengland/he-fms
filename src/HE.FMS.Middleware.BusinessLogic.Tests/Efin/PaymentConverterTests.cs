using System.Globalization;
using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Enums;
using HE.FMS.Middleware.Contract.Reclaims;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Efin;

public class PaymentConverterTests
{
    [Fact]
    public void GetDescription_ShouldReturnFormattedDescription_WhenMilestoneExists()
    {
        // Arrange
        var reclaimDetails = new ReclaimDetails { Milestone = Milestone.Acquisition, InvoiceId = "0000097R" };
        var applicationDetails = new ClaimApplicationDetails
        {
            ApplicationId = "12345",
            AllocationId = "G002186",
            SchemaName = "LongSchemaNameThatNeedsTruncation",
        };
        var milestoneLookup = new Dictionary<string, string> { { "Acquisition", "ACQ" } };

        // Act
        var description = PaymentConverter.GetDescription(reclaimDetails, reclaimDetails.Milestone, applicationDetails, milestoneLookup);

        // Assert
        Assert.Equal("ACQG002186 LongSchemaNameThatN", description);
    }

    [Fact]
    public void GetDescription_ShouldReturnFormattedDescription_WhenMilestoneDoesNotExist()
    {
        // Arrange
        var reclaimDetails = new ReclaimDetails { Milestone = null, InvoiceId = "0000098R" };
        var applicationDetails = new ClaimApplicationDetails
        {
            ApplicationId = "1234567",
            AllocationId = "G002186",
            SchemaName = "LongSchemaNameThatNeedsTruncation",
        };
        var milestoneLookup = new Dictionary<string, string> { { "Acquisition", "ACQ" } };

        // Act
        var description = PaymentConverter.GetDescription(reclaimDetails, reclaimDetails.Milestone, applicationDetails, milestoneLookup);

        // Assert
        Assert.Equal("LongSchemaName0000098RG002186", description);
    }

    [Fact]
    public void CalculateVatAmount_ShouldReturnCorrectVatAmount()
    {
        // Arrange
        var netAmount = 100m;
        var vatRate = 20m;

        // Act
        var vatAmount = PaymentConverter.CalculateVatAmount(netAmount, vatRate);

        // Assert
        Assert.Equal(20m, vatAmount);
    }

    [Fact]
    public void CalculateGrossAmount_ShouldReturnCorrectGrossAmount()
    {
        // Arrange
        var netAmount = 100m;
        var vatRate = 20m;

        // Act
        var grossAmount = PaymentConverter.CalculateGrossAmount(netAmount, vatRate);

        // Assert
        Assert.Equal(120m, grossAmount);
    }

    [Theory]
    [InlineData("2020/1/1", 2020)]
    [InlineData("2020/2/1", 2020)]
    [InlineData("2020/3/1", 2020)]
    [InlineData("2020/4/1", 2021)]
    [InlineData("2020/5/1", 2021)]
    [InlineData("2020/6/1", 2021)]
    [InlineData("2020/7/1", 2021)]
    [InlineData("2020/8/1", 2021)]
    [InlineData("2020/9/1", 2021)]
    [InlineData("2020/10/1", 2021)]
    [InlineData("2020/11/1", 2021)]
    [InlineData("2020/12/1", 2021)]
    public void GetAccountingYear_ShouldReturnCorrectYear(string date, int expected)
    {
        // Arrange
        var parsedDate = DateTime.Parse(date, CultureInfo.InvariantCulture);

        // Act
        var result = PaymentConverter.GetAccountingYear(parsedDate);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2020/1/1", 10)]
    [InlineData("2020/2/1", 11)]
    [InlineData("2020/3/1", 12)]
    [InlineData("2020/4/1", 1)]
    [InlineData("2020/5/1", 2)]
    [InlineData("2020/6/1", 3)]
    [InlineData("2020/7/1", 4)]
    [InlineData("2020/8/1", 5)]
    [InlineData("2020/9/1", 6)]
    [InlineData("2020/10/1", 7)]
    [InlineData("2020/11/1", 8)]
    [InlineData("2020/12/1", 9)]
    public void GetAccountingPeriod_ShouldReturnCorrectPeriod(string date, int expected)
    {
        // Arrange
        var parsedDate = DateTime.Parse(date, CultureInfo.InvariantCulture);

        // Act
        var result = PaymentConverter.GetAccountingPeriod(parsedDate);

        // Assert
        Assert.Equal(expected, result);
    }
}
