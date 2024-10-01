using FluentAssertions;
using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Tests.Factories;
using HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
using HE.FMS.Middleware.Contract.Claims.Efin;
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

    [Fact]
    public async Task CreateBatch_ShouldReturnClClb_Batch()
    {
        // Arrange  
        var claimPaymentRequest1 = PaymentRequestFactory.CreateRandomClaimPaymentRequest();
        var claimPaymentRequest2 = PaymentRequestFactory.CreateRandomClaimPaymentRequest();

        ClaimItem[] claims =
        [
            new()
            {
                CliInvoice = await _claimConverter.CreateCliInvoice(claimPaymentRequest1),
                ClaInvoiceAnalysis = await _claimConverter.CreateClaInvoiceAnalysis(claimPaymentRequest1),
            },
            new()
            {
                CliInvoice = await _claimConverter.CreateCliInvoice(claimPaymentRequest2),
                ClaInvoiceAnalysis = await _claimConverter.CreateClaInvoiceAnalysis(claimPaymentRequest2),
            },
        ];

        var batchRef = "BatchRef";

        // Act  
        var result = await _claimConverter.CreateBatch(claims, batchRef);

        // Assert  
        result.Should().NotBeNull();
        result.clb_sub_ledger.Should().Be("PL4");
        result.clb_batch_ref.Should().Be(batchRef);
        result.clb_year.Should().Be("1999");
        result.clb_period.Should().Be("1");
        result.clb_no_invoices.Should().Be("2");
        result.clb_quantity.Should().Be("2");
        result.clb_user.Should().Be("AH GRANTS");
        result.clb_grouping.Should().Be("C");
        result.clb_entry_date.Should().Be("1-Jan-00");
    }

    [Fact]
    public async Task CreateCliInvoice_ShouldReturnCLI_Invoice()
    {
        // Arrange  
        var claimPaymentRequest = PaymentRequestFactory.CreateRandomClaimPaymentRequest();

        // Act  
        var result = await _claimConverter.CreateCliInvoice(claimPaymentRequest);

        // Assert  
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateClaInvoiceAnalysis_ShouldReturnCLA_InvoiceAnalysis()
    {
        // Arrange  
        var claimPaymentRequest = PaymentRequestFactory.CreateRandomClaimPaymentRequest();

        // Act  
        var result = await _claimConverter.CreateClaInvoiceAnalysis(claimPaymentRequest);

        // Assert  
        result.Should().NotBeNull();
    }
}
