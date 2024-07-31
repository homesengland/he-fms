﻿using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

public enum TotalDuePayment
{
    [Description("FLAT")]
    Flat,

    [Description("OUTSTANDING_PRINCIPAL_PERCENTAGE")]
    OutstandingPrincipalPercentage,

    [Description("PRINCIPAL_PERCENTAGE_LAST_DISB")]
    PrincipalPercentageLastDisb,

    [Description("TOTAL_BALANCE_PERCENTAGE")]
    TotalBalancePercentage,

    [Description("TOTAL_BALANCE_FLAT")]
    TotalBalanceFlat,

    [Description("TOTAL_PRINCIPAL_PERCENTAGE")]
    TotalPrincipalPercentage,
}
