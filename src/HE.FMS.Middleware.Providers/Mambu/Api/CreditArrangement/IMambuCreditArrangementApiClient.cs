using HE.FMS.Middleware.Contract.Mambu.Common.Enums;
using HE.FMS.Middleware.Contract.Mambu.CreditArrangement;

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
