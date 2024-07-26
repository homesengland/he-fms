using HE.FMS.IntegrationPlatform.Contract.Grants.Results;
using HE.FMS.IntegrationPlatform.Contract.Grants.UseCases;
using HE.FMS.IntegrationPlatform.Domain.Framework;
using HE.FMS.IntegrationPlatform.Domain.Grants.Services;

namespace HE.FMS.IntegrationPlatform.Domain.Grants;

internal sealed class OpenNewGrantAccountUseCase : IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult>
{
    private readonly IOrganisationService _organisationService;

    public OpenNewGrantAccountUseCase(IOrganisationService organisationService)
    {
        _organisationService = organisationService;
    }

    public async Task<OpenNewGrantAccountResult> Trigger(OpenNewGrantAccountRequest input, CancellationToken cancellationToken)
    {
        await _organisationService.GetOrCreateOrganisation(input.Organisation, cancellationToken);

        return new OpenNewGrantAccountResult(
            input.ApplicationId,
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString());
    }
}
