using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group.Contract;
using Microsoft.Extensions.Logging;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group;

internal sealed class MambuGroupApiClient : MambuApiHttpClientBase, IMambuGroupApiClient
{
    public MambuGroupApiClient(HttpClient httpClient, ILogger<MambuGroupApiClient> logger)
        : base(httpClient, logger)
    {
    }

    protected override string ApiName => "Mambu.GroupApi";

    public async Task<IList<GetGroupResponse>> GetAll(CancellationToken cancellationToken)
    {
        return await Get<IList<GetGroupResponse>>("/api/groups", cancellationToken);
    }
}
