﻿using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Enums;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.DepositAccount.Contract;

public class DepositAccountReadDto
{
    public string Id { get; set; }

    public string AccountHolderKey { get; set; }

    public HolderType AccountHolderType { get; set; }
}
