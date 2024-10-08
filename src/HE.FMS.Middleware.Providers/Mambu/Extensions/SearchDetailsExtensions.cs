using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Contract.Extensions;
using HE.FMS.Middleware.Contract.Mambu.Common;

namespace HE.FMS.Middleware.Providers.Mambu.Extensions;

[ExcludeFromCodeCoverage]
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
