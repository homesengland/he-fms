using HE.FMS.Middleware.Providers.Mambu.Api.Common.Enums;
using HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement.Contract;

namespace HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement;

public interface IMambuCreditArrangementApiClient : IMambuRestApiClient<CreditArrangementDto, CreditArrangementReadDto, GetAllCreditArrangementsParams>
{
    Task<CreditArrangementAccountsDto> AddAccount(
        string creditArrangementId,
        string accountId,
        AccountType accountType,
        CancellationToken cancellationToken);

    Task<CreditArrangementAccountsDto> GetAccounts(string creditArrangementId, CancellationToken cancellationToken);
}
