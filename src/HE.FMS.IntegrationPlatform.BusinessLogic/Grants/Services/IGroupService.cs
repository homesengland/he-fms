using HE.FMS.IntegrationPlatform.Contract.Common;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group.Contract;

namespace HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Services;

internal interface IGroupService
{
    Task<GroupReadDto> GetOrCreateGroup(OrganisationContract organisation, CancellationToken cancellationToken);
}
