using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum InterestApplicationMethod
{
    [Description("AFTER_DISBURSEMENT")]
    AfterDisbursement,

    [Description("REPAYMENT_DUE_DATE")]
    RepaymentDueDate,

    [Description("FIXED_DAYS_OF_MONTH")]
    FixedDaysOfMonth,
}
