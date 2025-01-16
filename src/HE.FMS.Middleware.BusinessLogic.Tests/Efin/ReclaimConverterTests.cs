using System.Globalization;
using FluentAssertions;
using HE.FMS.Middleware.BusinessLogic.Constants;
using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Tests.Factories;
using HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
using HE.FMS.Middleware.Contract.Reclaims;
using HE.FMS.Middleware.Contract.Reclaims.Efin;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Efin;
public class ReclaimConverterTests
{
    private const string DateFormat = "d-MMM-yy";
    private const string DecimalFormat = "F";

    private readonly ReclaimConverter _reclaimConverter;
    private readonly FakeEfinLookupService _efinLookupService;
    private readonly FakeDateTimeProvider _dateTimeProvider;

    public ReclaimConverterTests()
    {
        _efinLookupService = new FakeEfinLookupService();
        _dateTimeProvider = new FakeDateTimeProvider();
        _reclaimConverter = new(_dateTimeProvider, _efinLookupService);
    }

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

    [Fact]
    public async Task CreateBatch_ShouldReturnCliIwBat()
    {
        // Arrange
        var defaultDictionary = await _efinLookupService.GetValue(EfinConstants.Lookups.ReclaimDefault);

        var reclaimPaymentRequest1 = PaymentRequestFactory.CreateRandomReclaimPaymentRequest();
        var reclaimPaymentRequest2 = PaymentRequestFactory.CreateRandomReclaimPaymentRequest();

        ReclaimItem[] reclaims =
        [
            new()
            {
                CliIwIlt = await _reclaimConverter.CreateCliIwIlt(reclaimPaymentRequest1),
                CliIwIna = await _reclaimConverter.CreateCliIwIna(reclaimPaymentRequest1),
                CliIwInl = await _reclaimConverter.CreateCliIwInl(reclaimPaymentRequest1),
                CliIwInv = await _reclaimConverter.CreateCliIwInv(reclaimPaymentRequest1),
                CliIwItl = await _reclaimConverter.CreateCliIwItl(reclaimPaymentRequest1),
            },
            new()
            {
                CliIwIlt = await _reclaimConverter.CreateCliIwIlt(reclaimPaymentRequest2),
                CliIwIna = await _reclaimConverter.CreateCliIwIna(reclaimPaymentRequest2),
                CliIwInl = await _reclaimConverter.CreateCliIwInl(reclaimPaymentRequest2),
                CliIwInv = await _reclaimConverter.CreateCliIwInv(reclaimPaymentRequest2),
                CliIwItl = await _reclaimConverter.CreateCliIwItl(reclaimPaymentRequest2),
            },
        ];

        var batchRef = "BatchRef";

        // Act
        var result = await _reclaimConverter.CreateBatch(reclaims, batchRef);

        // Assert
        result.Should().NotBeNull();
        result.cliwb_sub_ledger.Should().Be(defaultDictionary[nameof(CLI_IW_BAT.cliwb_sub_ledger)]);
        result.cliwb_batch_ref.Should().Be(batchRef);
        result.cliwb_year.Should().Be(PaymentConverter.GetAccountingYear(_dateTimeProvider.UtcNow).ToString(CultureInfo.InvariantCulture));
        result.cliwb_period.Should().Be(PaymentConverter.GetAccountingPeriod(_dateTimeProvider.UtcNow).ToString(CultureInfo.InvariantCulture));
        result.cliwb_no_invoices.Should().Be(reclaims.Length.ToString(CultureInfo.InvariantCulture));
        result.cliwb_user.Should().Be(defaultDictionary[nameof(CLI_IW_BAT.cliwb_user)]);
        result.cliwb_default_prefix.Should().Be(defaultDictionary[nameof(CLI_IW_BAT.cliwb_default_prefix)]);
        result.cliwb_entry_date.Should().Be(_dateTimeProvider.UtcNow.ToString(DateFormat, CultureInfo.InvariantCulture));
    }

    [Fact]
    public async Task CreateCliIwIlt_ShouldReturnCLI_IW_ILT()
    {
        // Arrange
        var defaultDictionary = await _efinLookupService.GetValue(EfinConstants.Lookups.ReclaimDefault);
        var request = PaymentRequestFactory.CreateRandomReclaimPaymentRequest();

        // Act
        var result = await _reclaimConverter.CreateCliIwIlt(request);

        // Assert
        result.Should().NotBeNull();
        result.cliwt_sub_ledger_id.Should().Be(defaultDictionary[nameof(CLI_IW_ILT.cliwt_sub_ledger_id)]);
        result.cliwt_inv_ref.Should().Be(request.Reclaim.InvoiceId);
        result.cliwt_batch_ref.Should().BeEmpty();
        result.cliwt_item_sequence.Should().Be(defaultDictionary[nameof(CLI_IW_ILT.cliwt_item_sequence)]);
        result.cliwt_print_sequence.Should().Be(defaultDictionary[nameof(CLI_IW_ILT.cliwt_print_sequence)]);
        result.cliwt_text.Should().Be(defaultDictionary[$"{nameof(CLI_IW_ILT.cliwt_text)}_{request.Application.Tenure}"]);
    }

    [Fact]
    public async Task CreateCliIwIna_ShouldReturnCLI_IW_INA()
    {
        // Arrange
        var defaultDictionary = await _efinLookupService.GetValue(EfinConstants.Lookups.ReclaimDefault);
        var regionLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.RegionLookup);
        var tenureLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.TenureLookup);
        var partnerTypeLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.PartnerTypeLookup);
        var request = PaymentRequestFactory.CreateRandomReclaimPaymentRequest();

        // Act
        var result = await _reclaimConverter.CreateCliIwIna(request);

        // Assert
        result.Should().NotBeNull();
        result.cliwa_sub_ledger_id.Should().Be(defaultDictionary[nameof(CLI_IW_INA.cliwa_sub_ledger_id)]);
        result.cliwa_inv_ref.Should().Be(request.Reclaim.InvoiceId);
        result.cliwa_batch_ref.Should().Be(string.Empty);
        result.cliwa_item_sequence.Should().Be(defaultDictionary[nameof(CLI_IW_INA.cliwa_item_sequence)]);
        result.cliwa_cost_centre.Should().Be(regionLookup[request.Application.Region.ToString()]);
        result.cliwa_account.Should().Be(request.Reclaim.Amount != 0 ?
            partnerTypeLookup[$"{EfinConstants.Lookups.Reclaim}_{nameof(ReclaimPaymentRequest.Reclaim.Amount)}_{request.Account.PartnerType}"] :
            partnerTypeLookup[$"{EfinConstants.Lookups.Reclaim}_{nameof(ReclaimPaymentRequest.Reclaim.InterestAmount)}_{request.Account.PartnerType}"]);
        result.cliwa_activity.Should().Be(tenureLookup[$"{EfinConstants.Lookups.Reclaim}_{request.Application.Tenure}"]);
        result.cliwa_job.Should().Be(defaultDictionary[nameof(CLI_IW_INA.cliwa_job)]);
    }

    [Fact]
    public async Task CreateCliIwInl_ShouldReturnCLI_IW_INL()
    {
        // Arrange
        var defaultDictionary = await _efinLookupService.GetValue(EfinConstants.Lookups.ReclaimDefault);
        var request = PaymentRequestFactory.CreateRandomReclaimPaymentRequest();

        // Act
        var result = await _reclaimConverter.CreateCliIwInl(request);

        // Assert
        result.Should().NotBeNull();
        result.cliwl_sub_ledger_id.Should().Be(defaultDictionary[nameof(CLI_IW_INL.cliwl_sub_ledger_id)]);
        result.cliwl_inv_ref.Should().Be(request.Reclaim.InvoiceId);
        result.cliwl_batch_ref.Should().Be(string.Empty);
        result.cliwl_item_sequence.Should().Be(defaultDictionary[nameof(CLI_IW_INL.cliwl_item_sequence)]);
        result.cliwl_product_id.Should().Be(defaultDictionary[nameof(CLI_IW_INL.cliwl_product_id)]);
        result.cliwl_goods_value.Should().Be(request.Reclaim.Amount.ToString(DecimalFormat, CultureInfo.InvariantCulture));
        result.cliwl_vat_code.Should().Be(((int)request.Application.VatCode).ToString("D2", CultureInfo.InvariantCulture));
        result.cliwl_vat_amount.Should().Be(PaymentConverter.CalculateVatAmount(request.Reclaim.Amount, request.Application.VatRate).ToString(DecimalFormat, CultureInfo.InvariantCulture));
        result.cliwl_line_ref.Should().Be(defaultDictionary[nameof(CLI_IW_INL.cliwl_line_ref)]);
    }

    [Fact]
    public async Task CreateCliIwInv_ShouldReturnCLI_IW_INV()
    {
        // Arrange
        var defaultDictionary = await _efinLookupService.GetValue(EfinConstants.Lookups.ReclaimDefault);
        var regionLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.RegionLookup);
        var tenureLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.TenureLookup);
        var partnerTypeLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.PartnerTypeLookup);
        var milestoneLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.MilestoneLookup);

        var request = PaymentRequestFactory.CreateRandomReclaimPaymentRequest();

        // Act
        var result = await _reclaimConverter.CreateCliIwInv(request);

        // Assert
        result.Should().NotBeNull();
        result.cliwi_sub_ledger_id.Should().Be(defaultDictionary[nameof(CLI_IW_INV.cliwi_sub_ledger_id)]);
        result.cliwi_inv_ref.Should().Be(request.Reclaim.InvoiceId);
        result.cliwi_batch_ref.Should().BeEmpty();
        result.cliwi_invoice_to_id.Should().Be(request.Account.ProviderId);
        result.cliwi_net_amount.Should().Be(request.Reclaim.Amount.ToString(DecimalFormat, CultureInfo.InvariantCulture));
        result.cliwi_their_ref.Should().Be(request.Application.AllocationId);
        result.cliwi_trans_type.Should().Be(defaultDictionary[nameof(CLI_IW_INV.cliwi_trans_type)]);
        result.cliwi_date.Should().Be(_dateTimeProvider.UtcNow.ToString(DateFormat, CultureInfo.InvariantCulture));
        result.cliwi_terms_code.Should().Be(defaultDictionary[nameof(CLI_IW_INV.cliwi_terms_code)]);
        result.cliwi_cost_centre.Should().Be(regionLookup[request.Application.Region.ToString()]);
        result.cliwi_job.Should().Be(defaultDictionary[nameof(CLI_IW_INV.cliwi_job)]);
        result.cliwi_account.Should().Be(request.Reclaim.Amount != 0
            ? partnerTypeLookup[$"{EfinConstants.Lookups.Reclaim}_Amount_{request.Account.PartnerType}"]
            : partnerTypeLookup[$"{EfinConstants.Lookups.Reclaim}_InterestAmount_{request.Account.PartnerType}"]);
        result.cliwi_activity.Should().Be(tenureLookup[$"{EfinConstants.Lookups.Reclaim}_{request.Application.Tenure}"]);
        result.cliwi_entry_date.Should().Be(_dateTimeProvider.UtcNow.ToString(DateFormat, CultureInfo.InvariantCulture));
        result.cliwi_invoice_prefix.Should().Be(defaultDictionary[nameof(CLI_IW_INV.cliwi_invoice_prefix)]);
        result.cliwi_tax_point.Should().Be(_dateTimeProvider.UtcNow.ToString(DateFormat, CultureInfo.InvariantCulture));
        result.cliwi_description.Should().Be(PaymentConverter.GetDescription(request.Reclaim, request.Reclaim.Milestone, request.Application, milestoneLookup));
    }

    [Fact]
    public async Task CreateCliIwItl_ShouldReturnCLI_IW_ITL()
    {
        // Arrange
        var defaultDictionary = await _efinLookupService.GetValue(EfinConstants.Lookups.ReclaimDefault);
        var milestoneLookup = await _efinLookupService.GetValue(EfinConstants.Lookups.MilestoneLookup);
        var request = PaymentRequestFactory.CreateRandomReclaimPaymentRequest();

        // Act
        var result = await _reclaimConverter.CreateCliIwItl(request);

        // Assert
        result.Should().NotBeNull();
        result.cliwx_sub_ledger_id.Should().Be(defaultDictionary[nameof(CLI_IW_ITL.cliwx_sub_ledger_id)]);
        result.cliwx_batch_ref.Should().BeEmpty();
        result.cliwx_inv_ref.Should().Be(request.Reclaim.InvoiceId);
        result.cliwx_line_no.Should().Be(defaultDictionary[nameof(CLI_IW_ITL.cliwx_line_no)]);
        result.cliwx_header_footer.Should().Be(defaultDictionary[nameof(CLI_IW_ITL.cliwx_header_footer)]);
        result.cliwx_text.Should().Be(PaymentConverter.GetDescription(request.Reclaim, request.Reclaim.Milestone, request.Application, milestoneLookup));
    }
}
