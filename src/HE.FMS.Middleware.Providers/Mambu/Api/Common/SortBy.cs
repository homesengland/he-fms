using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Providers.Mambu.Api.Common.Enums;

namespace HE.FMS.Middleware.Providers.Mambu.Api.Common;

public sealed record SortBy(IDictionary<string, SortDirection> Value)
{
    public string ToQueryParam()
    {
        return string.Join(",", Value.Select(x => $"{x.Key}:{x.Value.GetEnumDescription()}"));
    }
}
