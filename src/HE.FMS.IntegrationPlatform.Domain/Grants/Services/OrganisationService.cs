using HE.FMS.IntegrationPlatform.Contract.Common;
using HE.FMS.IntegrationPlatform.Domain.Grants.Mappers;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Enums;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group;

namespace HE.FMS.IntegrationPlatform.Domain.Grants.Services;

internal sealed class OrganisationService : IOrganisationService
{
    private readonly IMambuGroupApiClient _groupApiClient;

    private readonly IOrganisationMapper _organisationMapper;

    public OrganisationService(IMambuGroupApiClient groupApiClient, IOrganisationMapper organisationMapper)
    {
        _groupApiClient = groupApiClient;
        _organisationMapper = organisationMapper;
    }

    public async Task<OrganisationContract> GetOrCreateOrganisation(OrganisationContract organisation, CancellationToken cancellationToken)
    {
        var group = await _groupApiClient.GetById(organisation.Id, DetailsLevel.Full, cancellationToken)
                    ?? await _groupApiClient.Create(_organisationMapper.ToGroupDto(organisation), cancellationToken);

        return _organisationMapper.ToOrganisation(group);
    }
}
