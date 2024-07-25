using HE.FMS.IntegrationPlatform.Common.Extensions;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Enums;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common;

public sealed record SortBy(IDictionary<string, SortDirection> Value)
{
    public string ToQueryParam()
    {
        return string.Join(",", Value.Select(x => $"{x.Key}:{x.Value.GetEnumDescription()}"));
    }
}
