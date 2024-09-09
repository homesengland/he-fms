using HE.FMS.Middleware.Contract.Mambu.Group;

namespace HE.FMS.Middleware.Providers.Mambu.Api.Group;

public interface IMambuGroupApiClient : IMambuRestApiClient<GroupDto, GroupReadDto, GetAllGroupsParams>
{
}
