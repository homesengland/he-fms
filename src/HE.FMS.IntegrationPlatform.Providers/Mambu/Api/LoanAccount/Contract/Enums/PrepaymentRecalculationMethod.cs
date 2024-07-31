using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum PrepaymentRecalculationMethod
{
    [Description("NO_RECALCULATION")]
    NoRecalculation,

    [Description("RESCHEDULE_REMAINING_REPAYMENTS")]
    RescheduleRemainingRepayments,

    [Description("RECALCULATE_SCHEDULE_KEEP_SAME_NUMBER_OF_TERMS")]
    RecalculateScheduleKeepSameNumberOfTerms,

    [Description("RECALCULATE_SCHEDULE_KEEP_SAME_PRINCIPAL_AMOUNT")]
    RecalculateScheduleKeepSamePrincipalAmount,

    [Description("RECALCULATE_SCHEDULE_KEEP_SAME_TOTAL_REPAYMENT_AMOUNT")]
    RecalculateScheduleKeepSameTotalRepaymentAmount,

    [Description("REDUCE_AMOUNT_PER_INSTALLMENT")]
    ReduceAmountPerInstallment,

    [Description("REDUCE_NUMBER_OF_INSTALLMENTS")]
    ReduceNumberOfInstallments,

    [Description("REDUCE_NUMBER_OF_INSTALLMENTS_NEW")]
    ReduceNumberOfInstallmentsNew,
}
