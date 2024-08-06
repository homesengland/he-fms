using HE.FMS.Middleware.Contract.Grants;
using HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement.Contract;
using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;

namespace HE.FMS.Middleware.BusinessLogic.Grants.Services;

public interface ICreditArrangementService
{
    Task<CreditArrangementReadDto> GetOrCreateCreditArrangement(string applicationId, string groupId, GrantDetailsContract grantDetails, CancellationToken cancellationToken);

    Task<LoanAccountReadDto> AddLoanAccount(string creditArrangementId, string loanAccountId, CancellationToken cancellationToken);
}
