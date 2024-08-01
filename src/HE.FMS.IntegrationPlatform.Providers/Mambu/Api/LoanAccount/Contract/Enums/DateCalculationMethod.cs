﻿using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum DateCalculationMethod
{
    [Description("ACCOUNT_FIRST_WENT_TO_ARREARS")]
    AccountFirstWentToArrears,

    [Description("LAST_LATE_REPAYMENT")]

    LastLateRepayment,

    [Description("ACCOUNT_FIRST_BREACHED_MATERIALITY_THRESHOLD")]
    AccountFirstBreachedMaterialityThreshold,
}