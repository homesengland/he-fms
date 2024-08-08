﻿using System.ComponentModel;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum LockedAccountTotalDueType
{
    [Description("BALANCE_AMOUNT")]
    BalanceAmount,

    [Description("DUE_AMOUNT_ON_LATE_INSTALLMENTS")]
    DueAmountOnLateInstallments,
}