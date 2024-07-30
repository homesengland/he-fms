using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract;
using Microsoft.Extensions.Logging;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount;

internal sealed class MambuLoanAccountApiClient : MambuRestApiClientBase<LoanAccountDto, LoanAccountReadDto, GetAllLoanAccountsParams>, IMambuLoanAccountApiClient
{
    public MambuLoanAccountApiClient(HttpClient httpClient, ILogger<MambuLoanAccountApiClient> logger)
        : base(httpClient, logger)
    {
    }

    protected override string ApiUrl => "/api/loans";

    protected override string ApiName => "Mambu.LoanAccountApi";
}
