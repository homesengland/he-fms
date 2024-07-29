using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group.Contract;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group;

public interface IMambuGroupApiClient : IMambuRestApiClient<GroupDto, GroupReadDto, GetAllGroupsParams>
{
}
