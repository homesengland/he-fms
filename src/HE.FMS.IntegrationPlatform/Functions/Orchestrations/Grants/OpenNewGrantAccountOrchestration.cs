using HE.FMS.IntegrationPlatform.Contract.Grants.Results;
using HE.FMS.IntegrationPlatform.Contract.Grants.UseCases;
using HE.FMS.IntegrationPlatform.Functions.Activities.Grants.OpenNewGrantAccount;
using Microsoft.DurableTask;

namespace HE.FMS.IntegrationPlatform.Functions.Orchestrations.Grants;

[DurableTask(nameof(OpenNewGrantAccountOrchestration))]
public class OpenNewGrantAccountOrchestration : TaskOrchestrator<OpenNewGrantAccountRequest, OpenNewGrantAccountResult>
{
    public override async Task<OpenNewGrantAccountResult> RunAsync(TaskOrchestrationContext context, OpenNewGrantAccountRequest input)
    {
        var group = await context.CallCreateGroupAsync(input);
        var creditArrangement = await context.CallCreateCreditArrangementAsync(new CreateCreditArrangement.CreateCreditArrangementInput(input, group));
        var loanAccount = await context.CallCreateLoanAccountAsync(new CreateLoanAccount.CreateLoanAccountInput(input, group, creditArrangement));

        return new OpenNewGrantAccountResult(input.ApplicationId, creditArrangement.Id, loanAccount.Id);
    }
}
