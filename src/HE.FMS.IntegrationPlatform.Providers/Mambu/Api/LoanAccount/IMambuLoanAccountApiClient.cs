using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount;

public interface IMambuLoanAccountApiClient : IMambuRestApiClient<LoanAccountDto, LoanAccountReadDto, GetAllLoanAccountsParams>
{
}
