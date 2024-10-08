using HE.FMS.Middleware.Contract.Extensions;
using HE.FMS.Middleware.Contract.Mambu.Common;
using HE.FMS.Middleware.Contract.Mambu.Common.Enums;
using HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.LoanAccount;

public sealed record GetAllLoanAccountsParams(
    string? CreditOfficerUsername = null,
    string? BranchId = null,
    string? CentreId = null,
    AccountState? AccountState = null,
    HolderType? AccountHolderType = null,
    string? AccountHolderId = null,
    SortBy? SortBy = null) : IGetAllParams
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

        if (AccountState.HasValue)
        {
            yield return new KeyValuePair<string, string>("accountState", AccountState.Value.GetEnumDescription());
        }

        if (AccountHolderType.HasValue)
        {
            yield return new KeyValuePair<string, string>("accountHolderType", AccountHolderType.Value.GetEnumDescription());
        }

        if (!string.IsNullOrEmpty(AccountHolderId))
        {
            yield return new KeyValuePair<string, string>("accountHolderId", AccountHolderId);
        }

        if (SortBy is { Value.Count: > 0 })
        {
            yield return new KeyValuePair<string, string>("sortBy", SortBy.ToQueryParam());
        }
    }
}
