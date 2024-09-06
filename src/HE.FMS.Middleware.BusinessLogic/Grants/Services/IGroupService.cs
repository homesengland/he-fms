using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Contract.Mambu.Group;

namespace HE.FMS.Middleware.BusinessLogic.Grants.Services;

public interface IGroupService
{
    Task<GroupReadDto> GetOrCreateGroup(OrganisationContract organisation, CancellationToken cancellationToken);
}
