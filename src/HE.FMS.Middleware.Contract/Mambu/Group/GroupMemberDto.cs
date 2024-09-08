namespace HE.FMS.Middleware.Contract.Mambu.Group;

public sealed class GroupMemberDto
{
    public string ClientKey { get; set; }

    public IList<GroupRoleDto>? Roles { get; set; }
}
