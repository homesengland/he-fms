using HE.FMS.Middleware.Providers.Mambu.Api.Group.Contract;

namespace HE.FMS.Middleware.Providers.Mambu.Api.Group;

public interface IMambuGroupApiClient : IMambuRestApiClient<GroupDto, GroupReadDto, GetAllGroupsParams>
{
}
