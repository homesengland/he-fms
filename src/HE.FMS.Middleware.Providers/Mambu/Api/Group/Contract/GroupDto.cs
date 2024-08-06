namespace HE.FMS.Middleware.Providers.Mambu.Api.Group.Contract;

public class GroupDto
{
    public string Id { get; set; }

    public string GroupName { get; set; }

    public IList<GroupAddressDto>? Addresses { get; set; }

    public string? AssignedBranchKey { get; set; }

    public string? AssignedCentreKey { get; set; }

    public string? AssignedUserKey { get; set; }

    public string? EmailAddress { get; set; }

    public IList<GroupMemberDto>? GroupMembers { get; set; }

    public string? GroupRoleKey { get; set; }

    public string? HomePhone { get; set; }

    public string? MobilePhone { get; set; }

    public string? Notes { get; set; }

    public string? PreferredLanguage { get; set; }
}
