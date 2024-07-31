using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Enums;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement.Contract;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement;

public interface IMambuCreditArrangementApiClient : IMambuRestApiClient<CreditArrangementDto, CreditArrangementReadDto, GetAllCreditArrangementsParams>
{
    Task<CreditArrangementAccountsDto> AddAccount(
        string creditArrangementId,
        string accountId,
        AccountType accountType,
        CancellationToken cancellationToken);
}
