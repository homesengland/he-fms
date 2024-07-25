using System.Globalization;
using System.Web;
using HE.FMS.IntegrationPlatform.Common.Extensions;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Extensions;

public static class SearchDetailsExtensions
{
    public static string ToQueryString(this PageDetails pageDetails, IGetAllParams? getAllParams = null)
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        if (pageDetails.Offset.HasValue)
        {
            queryString.Add("offset", pageDetails.Offset.Value.ToString(CultureInfo.InvariantCulture));
        }

        if (pageDetails.Limit.HasValue)
        {
            queryString.Add("limit", pageDetails.Limit.Value.ToString(CultureInfo.InvariantCulture));
        }

        queryString.Add("paginationDetails", pageDetails.PaginationDetails.GetEnumDescription());
        queryString.Add("detailsLevel", pageDetails.DetailsLevel.GetEnumDescription());

        if (getAllParams != null)
        {
            foreach (var (key, value) in getAllParams.ToQueryParams())
            {
                queryString.Add(key, value);
            }
        }

        return queryString.ToString()!;
    }
}
