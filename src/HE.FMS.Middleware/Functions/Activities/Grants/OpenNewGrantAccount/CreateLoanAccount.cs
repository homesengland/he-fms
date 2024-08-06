using HE.FMS.Middleware.BusinessLogic.Grants.Services;
using HE.FMS.Middleware.Contract.Grants.UseCases;
using HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement.Contract;
using HE.FMS.Middleware.Providers.Mambu.Api.Group.Contract;
using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;
using Microsoft.DurableTask;

namespace HE.FMS.Middleware.Functions.Activities.Grants.OpenNewGrantAccount;

[DurableTask(nameof(CreateLoanAccount))]
public class CreateLoanAccount : TaskActivity<CreateLoanAccount.CreateLoanAccountInput, LoanAccountReadDto>
{
    private readonly ICreditArrangementService _creditArrangementService;

    private readonly ILoanAccountService _loanAccountService;

    public CreateLoanAccount(ICreditArrangementService creditArrangementService, ILoanAccountService loanAccountService)
    {
        _creditArrangementService = creditArrangementService;
        _loanAccountService = loanAccountService;
    }

    public override async Task<LoanAccountReadDto> RunAsync(TaskActivityContext context, CreateLoanAccountInput input)
    {
        var (loanAccount, accountAlreadyExists) = await _loanAccountService.GetOrCreateLoanAccount(
            input.CreditArrangement.EncodedKey,
            input.Group.EncodedKey,
            input.Request.GrantDetails,
            input.Request.PhaseDetails,
            CancellationToken.None);

        if (!accountAlreadyExists)
        {
            loanAccount = await _creditArrangementService.AddLoanAccount(
                input.CreditArrangement.EncodedKey,
                loanAccount.EncodedKey,
                CancellationToken.None);
        }

        return loanAccount;
    }

    public record CreateLoanAccountInput(OpenNewGrantAccountRequest Request, GroupReadDto Group, CreditArrangementReadDto CreditArrangement);
}
