using HE.FMS.Middleware.BusinessLogic.Grants.Services;
using HE.FMS.Middleware.Contract.Grants.UseCases;
using HE.FMS.Middleware.Providers.Mambu.Api.Group.Contract;
using Microsoft.DurableTask;

namespace HE.FMS.Middleware.Functions.Activities.Grants.OpenNewGrantAccount;

[DurableTask(nameof(CreateGroup))]
public class CreateGroup : TaskActivity<OpenNewGrantAccountRequest, GroupReadDto>
{
    private readonly IGroupService _groupService;

    public CreateGroup(IGroupService groupService)
    {
        _groupService = groupService;
    }

    public override async Task<GroupReadDto> RunAsync(TaskActivityContext context, OpenNewGrantAccountRequest input)
    {
        return await _groupService.GetOrCreateGroup(input.Organisation, CancellationToken.None);
    }
}
