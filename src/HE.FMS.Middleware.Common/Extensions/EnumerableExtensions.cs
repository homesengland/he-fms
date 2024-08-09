using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.FMS.Middleware.Common.Extensions;
public static class EnumerableExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
    {
        return collection == null || !collection.Any();
    }

    public static IEnumerable<T> DefaultIfNull<T>(this IEnumerable<T> collection)
    {
        if (collection.IsNullOrEmpty())
        {
            return Enumerable.Empty<T>();
        }

        return collection;
    }
}
