using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement.Contract;
using Microsoft.Extensions.Logging;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement;

internal sealed class MambuCreditArrangementApiClient : MambuRestApiClientBase<CreditArrangementDto, CreditArrangementReadDto, GetAllCreditArrangementsParams>, IMambuCreditArrangementApiClient
{
    public MambuCreditArrangementApiClient(HttpClient httpClient, ILogger<MambuCreditArrangementApiClient> logger)
        : base(httpClient, logger)
    {
    }

    protected override string ApiUrl => "/api/creditarrangements";

    protected override string ApiName => "Mambu.CreditArrangementsApi";
}
