using HE.FMS.Middleware.Contract.Mambu.Common.Enums;
using HE.FMS.Middleware.Contract.Mambu.CreditArrangement;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement;

internal sealed class MambuCreditArrangementApiClient : MambuRestApiClientBase<CreditArrangementDto, CreditArrangementReadDto, GetAllCreditArrangementsParams>, IMambuCreditArrangementApiClient
{
    public MambuCreditArrangementApiClient(HttpClient httpClient, ILogger<MambuCreditArrangementApiClient> logger)
        : base(httpClient, logger)
    {
    }

    protected override string ApiUrl => "/api/creditarrangements";

    protected override string ApiName => "Mambu.CreditArrangementsApi";

    public async Task<CreditArrangementAccountsDto> AddAccount(
        string creditArrangementId,
        string accountId,
        AccountType accountType,
        CancellationToken cancellationToken)
    {
        var relativeUrl = $"{ApiUrl}/{creditArrangementId}:addAccount";
        var requestBody = new AddCreditArrangementAccountDto { AccountId = accountId, AccountType = accountType, };

        return await Send<AddCreditArrangementAccountDto, CreditArrangementAccountsDto>(HttpMethod.Post, relativeUrl, requestBody, cancellationToken);
    }

    public async Task<CreditArrangementAccountsDto> GetAccounts(string creditArrangementId, CancellationToken cancellationToken)
    {
        return await Send<CreditArrangementAccountsDto>(HttpMethod.Get, $"{ApiUrl}/{creditArrangementId}/accounts", cancellationToken);
    }
}
