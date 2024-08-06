using HE.FMS.Middleware.Providers.Mambu.Api.Common;

namespace HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement.Contract;

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
