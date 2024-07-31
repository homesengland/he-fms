using HE.FMS.IntegrationPlatform.Contract.Grants;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement.Contract;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract;

namespace HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Services;

public interface ICreditArrangementService
{
    Task<CreditArrangementReadDto> GetOrCreateCreditArrangement(string applicationId, string groupId, GrantDetailsContract grantDetails, CancellationToken cancellationToken);

    Task<LoanAccountReadDto> AddLoanAccount(string creditArrangementId, string loanAccountId, CancellationToken cancellationToken);
}
