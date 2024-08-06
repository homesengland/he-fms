using HE.FMS.Middleware.BusinessLogic.Grants.Services;
using HE.FMS.Middleware.Contract.Grants.UseCases;
using HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement.Contract;
using HE.FMS.Middleware.Providers.Mambu.Api.Group.Contract;
using Microsoft.DurableTask;

namespace HE.FMS.Middleware.Functions.Activities.Grants.OpenNewGrantAccount;

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
