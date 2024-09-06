using HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount;

public sealed class ScheduleSettingsDto
{
    public int? AmortizationPeriod { get; set; }

    public BillingCycleDaysDto? BillingCycle { get; set; }

    public int? DefaultFirstRepaymentDueDateOffset { get; set; }

    public IList<int>? FixedDaysOfMonth { get; set; }

    public int GracePeriod { get; set; }

    public GracePeriodType? GracePeriodType { get; set; }

    public bool? HasCustomSchedule { get; set; }

    public IList<PeriodicPaymentDto>? PaymentPlan { get; set; }

    public decimal? PeriodicPayment { get; set; }

    public RevolvingAccountSettingsDto? PreviewSchedule { get; set; }

    public int? PrincipalRepaymentInterval { get; set; }

    public int? RepaymentInstallments { get; set; }

    public int? RepaymentPeriodCount { get; set; }

    public RepaymentPeriodUnit? RepaymentPeriodUnit { get; set; }

    public RepaymentScheduleMethod? RepaymentScheduleMethod { get; set; }

    public ScheduleDueDatesMethod? ScheduleDueDatesMethod { get; set; }

    public ShortMonthHandlingMethod? ShortMonthHandlingMethod { get; set; }
}
