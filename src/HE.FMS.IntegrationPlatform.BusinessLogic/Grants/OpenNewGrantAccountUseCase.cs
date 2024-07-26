using HE.FMS.IntegrationPlatform.BusinessLogic.Framework;
using HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Services;
using HE.FMS.IntegrationPlatform.Contract.Grants.Results;
using HE.FMS.IntegrationPlatform.Contract.Grants.UseCases;

namespace HE.FMS.IntegrationPlatform.BusinessLogic.Grants;

internal sealed class OpenNewGrantAccountUseCase : IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult>
{
    private readonly IGroupService _groupService;

    private readonly ICreditArrangementService _creditArrangementService;

    public OpenNewGrantAccountUseCase(IGroupService groupService, ICreditArrangementService creditArrangementService)
    {
        _groupService = groupService;
        _creditArrangementService = creditArrangementService;
    }

    public async Task<OpenNewGrantAccountResult> Trigger(OpenNewGrantAccountRequest input, CancellationToken cancellationToken)
    {
        var group = await _groupService.GetOrCreateGroup(input.Organisation, cancellationToken);
        var creditArrangement = await _creditArrangementService.GetOrCreateCreditArrangement(
            input.ApplicationId,
            group.EncodedKey,
            input.GrantDetails,
            cancellationToken);

        return new OpenNewGrantAccountResult(
            input.ApplicationId,
            group.Id,
            creditArrangement.Id);
    }
}
