using HE.FMS.Middleware.Contract.Grants;
using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;

namespace HE.FMS.Middleware.BusinessLogic.Grants.Services;

public interface ILoanAccountService
{
    Task<(LoanAccountReadDto Account, bool AccountAlreadyExists)> GetOrCreateLoanAccount(
        string creditArrangementId,
        string groupId,
        GrantDetailsContract grantDetails,
        PhaseDetailsContract phaseDetails,
        CancellationToken cancellationToken);
}
