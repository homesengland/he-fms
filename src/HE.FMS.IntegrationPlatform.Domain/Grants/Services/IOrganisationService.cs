using HE.FMS.IntegrationPlatform.Contract.Common;

namespace HE.FMS.IntegrationPlatform.Domain.Grants.Services;

internal interface IOrganisationService
{
    Task<OrganisationContract> GetOrCreateOrganisation(OrganisationContract organisation, CancellationToken cancellationToken);
}
