using HE.FMS.IntegrationPlatform.BusinessLogic.Framework;
using HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Services;
using HE.FMS.IntegrationPlatform.Contract.Grants.Results;
using HE.FMS.IntegrationPlatform.Contract.Grants.UseCases;

namespace HE.FMS.IntegrationPlatform.BusinessLogic.Grants;

internal sealed class OpenNewGrantAccountUseCase : IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult>
{
    private readonly IGroupService _groupService;

    private readonly ICreditArrangementService _creditArrangementService;

    private readonly ILoanAccountService _loanAccountService;

    public OpenNewGrantAccountUseCase(IGroupService groupService, ICreditArrangementService creditArrangementService, ILoanAccountService loanAccountService)
    {
        _groupService = groupService;
        _creditArrangementService = creditArrangementService;
        _loanAccountService = loanAccountService;
    }

    public async Task<OpenNewGrantAccountResult> Trigger(OpenNewGrantAccountRequest input, CancellationToken cancellationToken)
    {
        var group = await _groupService.GetOrCreateGroup(input.Organisation, cancellationToken);
        var creditArrangement = await _creditArrangementService.GetOrCreateCreditArrangement(
            input.ApplicationId,
            group.EncodedKey,
            input.GrantDetails,
            cancellationToken);
        var (loanAccount, accountAlreadyExists) = await _loanAccountService.GetOrCreateLoanAccount(
            creditArrangement.EncodedKey,
            group.EncodedKey,
            input.GrantDetails,
            input.PhaseDetails,
            cancellationToken);

        if (!accountAlreadyExists)
        {
            loanAccount = await _creditArrangementService.AddLoanAccount(
                creditArrangement.EncodedKey,
                loanAccount.EncodedKey,
                cancellationToken);
        }

        return new OpenNewGrantAccountResult(
            input.ApplicationId,
            creditArrangement.Id,
            loanAccount.Id);
    }
}
