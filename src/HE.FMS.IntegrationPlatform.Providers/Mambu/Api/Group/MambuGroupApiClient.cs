using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group.Contract;
using Microsoft.Extensions.Logging;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group;

internal sealed class MambuGroupApiClient : MambuRestApiClientBase<GroupDto, GroupReadDto, GetAllGroupsParams>, IMambuGroupApiClient
{
    public MambuGroupApiClient(HttpClient httpClient, ILogger<MambuGroupApiClient> logger)
        : base(httpClient, logger)
    {
    }

    protected override string ApiUrl => "/api/groups";

    protected override string ApiName => "Mambu.GroupApi";
}
