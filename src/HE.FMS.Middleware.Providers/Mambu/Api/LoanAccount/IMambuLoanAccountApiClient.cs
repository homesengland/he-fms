using HE.FMS.Middleware.Contract.Mambu.LoanAccount;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount;

public interface IMambuLoanAccountApiClient : IMambuRestApiClient<LoanAccountDto, LoanAccountReadDto, GetAllLoanAccountsParams>
{
}
