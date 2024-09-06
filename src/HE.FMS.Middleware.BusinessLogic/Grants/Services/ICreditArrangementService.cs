using HE.FMS.Middleware.Contract.Grants;
using HE.FMS.Middleware.Contract.Mambu.CreditArrangement;
using HE.FMS.Middleware.Contract.Mambu.LoanAccount;

namespace HE.FMS.Middleware.BusinessLogic.Grants.Services;

public interface ICreditArrangementService
{
    Task<CreditArrangementReadDto> GetOrCreateCreditArrangement(string applicationId, string groupId, GrantDetailsContract grantDetails, CancellationToken cancellationToken);

    Task<LoanAccountReadDto> AddLoanAccount(string creditArrangementId, string loanAccountId, CancellationToken cancellationToken);
}
