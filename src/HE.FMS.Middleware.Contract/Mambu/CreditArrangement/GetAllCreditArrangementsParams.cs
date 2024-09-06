using HE.FMS.Middleware.Contract.Mambu.Common;

namespace HE.FMS.Middleware.Contract.Mambu.CreditArrangement;

public sealed record GetAllCreditArrangementsParams(SortBy? SortBy = null) : IGetAllParams
{
    public IEnumerable<KeyValuePair<string, string>> ToQueryParams()
    {
        if (SortBy is { Value.Count: > 0 })
        {
            yield return new KeyValuePair<string, string>("sortBy", SortBy.ToQueryParam());
        }
    }
}
