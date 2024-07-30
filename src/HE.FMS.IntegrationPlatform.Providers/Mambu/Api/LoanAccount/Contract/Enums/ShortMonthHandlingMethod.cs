﻿using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum ShortMonthHandlingMethod
{
    [Description("LAST_DAY_IN_MONTH")]
    LastDayInMonth,

    [Description("FIRST_DAY_OF_NEXT_MONTH")]
    FirstDayOfNextMonth,
}
