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
    private readonly FakeDateTimeProvider _dateTimeProvider;
    private readonly ClaimConverter _claimConverter;

    public ClaimConverterTests()
    {
        _efinLookupService = new FakeEfinLookupService();
        _dateTimeProvider = new FakeDateTimeProvider();
        _claimConverter = new ClaimConverter(_dateTimeProvider, _efinLookupService);
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
        var defaultDictionary = await _efinLookupService.GetValue(EfinConstants.Lookups.ClaimDefault);

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
        result.clb_sub_ledger.Should().Be(defaultDictionary[nameof(CLCLB_Batch.clb_sub_ledger)]);
        result.clb_batch_ref.Should().Be(batchRef);
        result.clb_year.Should().Be(PaymentConverter.GetAccountingYear(_dateTimeProvider.UtcNow).ToString(CultureInfo.InvariantCulture));
        result.clb_period.Should().Be(PaymentConverter.GetAccountingPeriod(_dateTimeProvider.UtcNow).ToString(CultureInfo.InvariantCulture));
        result.clb_no_invoices.Should().Be(claims.Length.ToString(CultureInfo.InvariantCulture));
        result.clb_quantity.Should().Be(claims.Length.ToString(CultureInfo.InvariantCulture));
        result.clb_user.Should().Be(defaultDictionary[nameof(CLCLB_Batch.clb_user)]);
        result.clb_grouping.Should().Be(defaultDictionary[nameof(CLCLB_Batch.clb_grouping)]);
        result.clb_entry_date.Should().Be(_dateTimeProvider.UtcNow.ToString(DateFormat, CultureInfo.InvariantCulture));
    }

    [Fact]
    public async Task CreateCliInvoice_ShouldReturnCLI_Invoice()
    {
        // Arrange
        var defaultDictionary = await _efinLookupService.GetValue(EfinConstants.Lookups.ClaimDefault);
        var regionLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.RegionLookup);
        var tenureLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.TenureLookup);
        var milestoneLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.MilestoneLookup);

        var request = PaymentRequestFactory.CreateRandomClaimPaymentRequest();

        // Act
        var result = await _claimConverter.CreateCliInvoice(request);

        // Assert
        result.Should().NotBeNull();

        result.cli_sub_ledger.Should().Be(defaultDictionary[nameof(CLI_Invoice.cli_sub_ledger)]);
        result.cli_inv_ref.Should().Be(request.Claim.InvoiceId);
        result.cli_batch_ref.Should().BeEmpty();
        result.cli_cfacs_customer.Should().Be(request.Account.ProviderId);
        result.cli_net_amount.Should().Be(request.Claim.Amount.ToString(DecimalFormat, CultureInfo.InvariantCulture));
        result.cli_vat.Should().Be((request.Claim.Amount * request.Application.VatRate / 100).ToString(DecimalFormat, CultureInfo.InvariantCulture));
        result.cli_gross.Should().Be((request.Claim.Amount + (request.Claim.Amount * (request.Application.VatRate / 100))).ToString(DecimalFormat, CultureInfo.InvariantCulture));
        result.cli_volume.Should().Be(defaultDictionary[nameof(CLI_Invoice.cli_volume)]);
        result.cli_uom.Should().Be(defaultDictionary[nameof(CLI_Invoice.cli_uom)]);
        result.cli_our_ref_2.Should().Be(request.Application.AllocationId);
        result.cli_their_ref.Should().Be(request.Claim.InvoiceId);
        result.cli_trans_type.Should().Be(defaultDictionary[nameof(CLI_Invoice.cli_trans_type)]);
        result.cli_date.Should().Be(request.Claim.ApprovedOn.ToString(DateFormat, CultureInfo.InvariantCulture));
        result.cli_terms_code.Should().Be(defaultDictionary[nameof(CLI_Invoice.cli_terms_code)]);
        result.cli_due_date.Should().Be(request.Claim.ApprovedOn.AddDays(7).ToString(DateFormat, CultureInfo.InvariantCulture));
        result.cli_cost_centre.Should().Be(regionLookup[request.Application.Region.ToString()]);
        result.cli_job.Should().Be(defaultDictionary[nameof(CLI_Invoice.cli_job)]);
        result.cli_activity.Should().Be(tenureLookup[$"{EfinConstants.Lookups.Claim}_{request.Application.Tenure}"]);
        result.cli_description.Should().Be(PaymentConverter.GetDescription(request.Claim, request.Claim.Milestone, request.Application, milestoneLookup));
    }

    [Fact]
    public async Task CreateClaInvoiceAnalysis_ShouldReturnCLA_InvoiceAnalysis()
    {
        // Arrange
        var defaultDictionary = await _efinLookupService.GetValue(EfinConstants.Lookups.ClaimDefault);
        var regionLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.RegionLookup);
        var tenureLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.TenureLookup);
        var partnerTypeLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.PartnerTypeLookup);
        var milestoneLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.MilestoneLookup);

        var request = PaymentRequestFactory.CreateRandomClaimPaymentRequest();

        // Act
        var result = await _claimConverter.CreateClaInvoiceAnalysis(request);

        // Assert
        result.Should().NotBeNull();

        result.cla_sub_ledger.Should().Be(defaultDictionary[nameof(CLA_InvoiceAnalysis.cla_sub_ledger)]);
        result.cla_inv_ref.Should().Be(request.Claim.InvoiceId);
        result.cla_batch_ref.Should().Be(string.Empty);
        result.cla_cfacs_cc.Should().Be(regionLookup[request.Application.Region.ToString()]);
        result.cla_cfacs_ac.Should().Be(partnerTypeLookup[$"{EfinConstants.Lookups.Claim}_{request.Application.RevenueIndicator}_{request.Account.PartnerType}"]);
        result.cla_cfacs_actv.Should().Be(tenureLookup[$"{EfinConstants.Lookups.Claim}_{request.Application.Tenure}"]);
        result.cla_cfacs_job.Should().Be(defaultDictionary[nameof(CLA_InvoiceAnalysis.cla_cfacs_job)]);
        result.cla_amount.Should().Be(request.Claim.Amount.ToString(DecimalFormat, CultureInfo.InvariantCulture));
        result.cla_vat_code.Should().Be(((int)request.Application.VatCode).ToString("D2", CultureInfo.InvariantCulture));
        result.cla_vat_rate.Should().Be(request.Application.VatRate.ToString(DecimalFormat, CultureInfo.InvariantCulture));
        result.cla_vat.Should().Be((request.Claim.Amount * request.Application.VatRate / 100).ToString(DecimalFormat, CultureInfo.InvariantCulture));
        result.cla_unit_qty.Should().Be(defaultDictionary[nameof(CLA_InvoiceAnalysis.cla_unit_qty)]);
        result.cla_uom.Should().Be(defaultDictionary[nameof(CLA_InvoiceAnalysis.cla_uom)]);
        result.cla_volume.Should().Be(defaultDictionary[nameof(CLA_InvoiceAnalysis.cla_volume)]);
        result.cla_description.Should().Be(PaymentConverter.GetDescription(request.Claim, request.Claim.Milestone, request.Application, milestoneLookup));
    }
}
