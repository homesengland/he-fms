using System.Diagnostics.CodeAnalysis;
using HE.FMS.Middleware.Contract.Mambu.LoanAccount;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount;

[ExcludeFromCodeCoverage]
internal sealed class MambuLoanAccountApiClient : MambuRestApiClientBase<LoanAccountDto, LoanAccountReadDto, GetAllLoanAccountsParams>, IMambuLoanAccountApiClient
{
    public MambuLoanAccountApiClient(HttpClient httpClient, ILogger<MambuLoanAccountApiClient> logger)
        : base(httpClient, logger)
    {
    }

    protected override string ApiUrl => "/api/loans";

    protected override string ApiName => "Mambu.LoanAccountApi";
}
