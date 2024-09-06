using System.ComponentModel;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

public enum DateCalculationMethod
{
    [Description("ACCOUNT_FIRST_WENT_TO_ARREARS")]
    AccountFirstWentToArrears,

    [Description("LAST_LATE_REPAYMENT")]

    LastLateRepayment,

    [Description("ACCOUNT_FIRST_BREACHED_MATERIALITY_THRESHOLD")]
    AccountFirstBreachedMaterialityThreshold,
}
