using HE.FMS.Middleware.Providers.Mambu.Api.Common.Contract;
using HE.FMS.Middleware.Providers.Mambu.Api.Common.Enums;
using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;

public class LoanAccountDto
{
    public string Id { get; set; }

    public string AccountHolderKey { get; set; }

    public HolderType AccountHolderType { get; set; }

    public AccountState? AccountState { get; set; }

    public AccountSubState? AccountSubState { get; set; }

    public AccountArrearsSettingsDto? AccountArrearsSettings { get; set; }

    public decimal? AccruedInterest { get; set; }

    public decimal? AccruedPenalty { get; set; }

    public string? ActivationTransactionKey { get; set; }

    public bool? AllowOffset { get; set; }

    public int? ArrearsTolerancePeriod { get; set; }

    public IList<AssetDto>? Assets { get; set; }

    public string? AssignedBranchKey { get; set; }

    public string? AssignedCentreKey { get; set; }

    public string? AssignedUserKey { get; set; }

    public BalancesDto? Balances { get; set; }

    public string CreditArrangementKey { get; set; }

    public CurrencyDto? Currency { get; set; }

    public DisbursementDetailsDto? DisbursementDetails { get; set; }

    public IList<InvestorFund>? FundingSources { get; set; }

    public string? FuturePaymentsAcceptance { get; set; }

    public IList<GuarantorDto>? Guarantors { get; set; }

    public decimal? InterestCommission { get; set; }

    public InterestSettingsDto InterestSettings { get; set; }

    public decimal LoanAmount { get; set; }

    public string LoanName { get; set; }

    public string? LockedAccountTotalDueType { get; set; }

    public IList<string>? LockedOperations { get; set; }

    public string? MigrationEventKey { get; set; }

    public string? Notes { get; set; }

    public string? OriginalAccountKey { get; set; }

    public decimal? PaymentHolidaysAccruedInterest { get; set; }

    public PaymentMethod? PaymentMethod { get; set; }

    public PenaltySettingsDto? PenaltySettings { get; set; }

    public IList<PlannedInstallmentFeeDto>? PlannedInstallmentFees { get; set; }

    public PrepaymentSettingsDto? PrepaymentSettings { get; set; }

    public PrincipalPaymentAccountSettingsDto? PrincipalPaymentSettings { get; set; }

    public string ProductTypeKey { get; set; }

    public LoanAccountRedrawSettingsDto? RedrawSettings { get; set; }

    public string? RescheduledAccountKey { get; set; }

    public ScheduleSettingsDto ScheduleSettings { get; set; }

    public string? SettlementAccountKey { get; set; }

    public decimal? TaxRate { get; set; }

    public DateTimeOffset? TerminationDate { get; set; }

    public IList<LoanTrancheDto>? Tranches { get; set; }
}
