using HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Services;
using HE.FMS.IntegrationPlatform.Contract.Grants.UseCases;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement.Contract;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group.Contract;
using Microsoft.DurableTask;

namespace HE.FMS.IntegrationPlatform.Functions.Activities.Grants.OpenNewGrantAccount;

[DurableTask(nameof(CreateCreditArrangement))]
public class CreateCreditArrangement : TaskActivity<CreateCreditArrangement.CreateCreditArrangementInput, CreditArrangementReadDto>
{
    private readonly ICreditArrangementService _creditArrangementService;

    public CreateCreditArrangement(ICreditArrangementService creditArrangementService)
    {
        _creditArrangementService = creditArrangementService;
    }

    public override async Task<CreditArrangementReadDto> RunAsync(TaskActivityContext context, CreateCreditArrangementInput input)
    {
        return await _creditArrangementService.GetOrCreateCreditArrangement(
            input.Request.ApplicationId,
            input.Group.EncodedKey,
            input.Request.GrantDetails,
            CancellationToken.None);
    }

    public record CreateCreditArrangementInput(OpenNewGrantAccountRequest Request, GroupReadDto Group);
}
