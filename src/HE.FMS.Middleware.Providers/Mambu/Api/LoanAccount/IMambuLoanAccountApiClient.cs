using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount;

public interface IMambuLoanAccountApiClient : IMambuRestApiClient<LoanAccountDto, LoanAccountReadDto, GetAllLoanAccountsParams>
{
}
