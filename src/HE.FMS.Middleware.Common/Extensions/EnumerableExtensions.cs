namespace HE.FMS.Middleware.Common.Extensions;
public static class EnumerableExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
    {
        return collection is null || !collection.Any();
    }

    public static IEnumerable<T> DefaultIfNull<T>(this IEnumerable<T> collection)
    {
        if (collection.IsNullOrEmpty())
        {
            return Enumerable.Empty<T>();
        }

        return collection;
    }

    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> collection)
    {
        return from item in collection
               where item is not null
               select item;
    }

    public static IEnumerable<T> AsEnumerable<T>(this T item)
    {
        return [item];
    }
}
