namespace HE.FMS.Middleware.Providers.Mambu.Api.Group.Contract;

public sealed class GroupMemberDto
{
    public string ClientKey { get; set; }

    public IList<GroupRoleDto>? Roles { get; set; }
}
