using HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Services;
using HE.FMS.IntegrationPlatform.Contract.Grants.UseCases;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group.Contract;
using Microsoft.DurableTask;

namespace HE.FMS.IntegrationPlatform.Functions.Activities.Grants.OpenNewGrantAccount;

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
