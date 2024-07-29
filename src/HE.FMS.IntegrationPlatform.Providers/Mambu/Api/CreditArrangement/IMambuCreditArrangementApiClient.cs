using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement.Contract;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement;

public interface IMambuCreditArrangementApiClient : IMambuRestApiClient<CreditArrangementDto, CreditArrangementReadDto, GetAllCreditArrangementsParams>
{
}
