using System.Globalization;
using FluentAssertions;
using HE.FMS.Middleware.BusinessLogic.Constants;
using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Tests.Factories;
using HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
using HE.FMS.Middleware.Contract.Claims.Efin;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Efin;

public class ClaimConverterTests
{
    private const string DateFormat = "d-MMM-yy";
    private const string DecimalFormat = "F";

    private readonly FakeEfinLookupService _efinLookupService;
    private readonly ClaimConverter _claimConverter;

    public ClaimConverterTests()
    {
        _efinLookupService = new FakeEfinLookupService();
        var dateTimeProvider = new FakeDateTimeProvider();
        _claimConverter = new ClaimConverter(dateTimeProvider, _efinLookupService);
    }

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
        var defaultDictionary = await _efinLookupService.GetValue(EfinConstants.Lookups.ClaimDefault);
        var regionLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.RegionLookup);
        var tenureLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.TenureLookup);

        var request = PaymentRequestFactory.CreateRandomClaimPaymentRequest();

        // Act  
        var result = await _claimConverter.CreateCliInvoice(request);

        // Assert  
        result.Should().NotBeNull();

        result.cli_sub_ledger.Should().Be(defaultDictionary[nameof(CLI_Invoice.cli_sub_ledger)]);
        result.cli_inv_ref.Should().Be(request.Application.AllocationId);
        result.cli_batch_ref.Should().BeEmpty();
        result.cli_cfacs_customer.Should().Be(request.Account.ProviderId);
        result.cli_net_amount.Should().Be(request.Claim.Amount.ToString(DecimalFormat, CultureInfo.InvariantCulture));
        result.cli_vat.Should().Be((request.Claim.Amount * request.Application.VatRate / 100).ToString(DecimalFormat, CultureInfo.InvariantCulture));
        result.cli_gross.Should().Be((request.Claim.Amount + (request.Claim.Amount * (request.Application.VatRate / 100))).ToString(DecimalFormat, CultureInfo.InvariantCulture));
        result.cli_volume.Should().Be(defaultDictionary[nameof(CLI_Invoice.cli_volume)]);
        result.cli_uom.Should().Be(defaultDictionary[nameof(CLI_Invoice.cli_uom)]);
        result.cli_our_ref_2.Should().Be(request.Application.AllocationId);
        result.cli_their_ref.Should().Be(request.Application.AllocationId);
        result.cli_trans_type.Should().Be(defaultDictionary[nameof(CLI_Invoice.cli_trans_type)]);
        result.cli_date.Should().Be(request.Claim.ApprovedOn.ToString(DateFormat, CultureInfo.InvariantCulture));
        result.cli_terms_code.Should().Be(defaultDictionary[nameof(CLI_Invoice.cli_terms_code)]);
        result.cli_due_date.Should().Be(request.Claim.ApprovedOn.AddDays(7).ToString(DateFormat, CultureInfo.InvariantCulture));
        result.cli_cost_centre.Should().Be(regionLookup[request.Application.Region.ToString()]);
        result.cli_job.Should().Be(defaultDictionary[nameof(CLI_Invoice.cli_job)]);
        result.cli_activity.Should().Be(tenureLookup[request.Application.Tenure.ToString()]);
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
