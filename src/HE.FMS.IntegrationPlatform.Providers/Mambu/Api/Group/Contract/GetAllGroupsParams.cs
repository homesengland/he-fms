using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group.Contract;

public sealed record GetAllGroupsParams(string? CreditOfficerUsername = null, string? BranchId = null, string? CentreId = null, SortBy? SortBy = null) : IGetAllParams
{
    public IEnumerable<KeyValuePair<string, string>> ToQueryParams()
    {
        if (!string.IsNullOrEmpty(CreditOfficerUsername))
        {
            yield return new KeyValuePair<string, string>("creditOfficerUsername", CreditOfficerUsername);
        }

        if (!string.IsNullOrEmpty(BranchId))
        {
            yield return new KeyValuePair<string, string>("branchId", BranchId);
        }

        if (!string.IsNullOrEmpty(CentreId))
        {
            yield return new KeyValuePair<string, string>("centreId", CentreId);
        }

        if (SortBy is { Value.Count: > 0 })
        {
            yield return new KeyValuePair<string, string>("sortBy", SortBy.ToQueryParam());
        }
    }
}
