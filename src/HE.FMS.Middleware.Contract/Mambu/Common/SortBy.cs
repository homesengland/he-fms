using HE.FMS.Middleware.Contract.Extensions;
using HE.FMS.Middleware.Contract.Mambu.Common.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.Common;

public sealed record SortBy(IDictionary<string, SortDirection> Value)
{
    public string ToQueryParam()
    {
        return string.Join(",", Value.Select(x => $"{x.Key}:{x.Value.GetEnumDescription()}"));
    }
}
