using HE.FMS.IntegrationPlatform.Contract.Common;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group.Contract;

namespace HE.FMS.IntegrationPlatform.Domain.Grants.Mappers;

internal interface IOrganisationMapper
{
    GroupDto ToGroupDto(OrganisationContract organisation);

    OrganisationContract ToOrganisation(GroupDto group);
}
