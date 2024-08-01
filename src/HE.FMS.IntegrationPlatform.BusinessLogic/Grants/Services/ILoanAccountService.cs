﻿using HE.FMS.IntegrationPlatform.Contract.Grants;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract;

namespace HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Services;

public interface ILoanAccountService
{
    Task<(LoanAccountReadDto Account, bool AccountAlreadyExists)> GetOrCreateLoanAccount(
        string creditArrangementId,
        string groupId,
        GrantDetailsContract grantDetails,
        PhaseDetailsContract phaseDetails,
        CancellationToken cancellationToken);
}