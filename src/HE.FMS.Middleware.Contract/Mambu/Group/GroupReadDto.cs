namespace HE.FMS.Middleware.Contract.Mambu.Group;

public sealed class GroupReadDto : GroupDto
{
    public DateTimeOffset CreationDate { get; set; }

    public string EncodedKey { get; set; }

    public DateTimeOffset? LastModifiedDate { get; set; }

    public int? LoanCycle { get; set; }

    public string? MigrationEventKey { get; set; }
}
