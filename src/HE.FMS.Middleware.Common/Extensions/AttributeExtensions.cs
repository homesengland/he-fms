using System.Reflection;

namespace HE.FMS.Middleware.Common.Extensions;
public static class AttributeExtensions
{
    public static TValue? GetClassAttributeValue<TAttribute, TValue>(
        this Type type,
        Func<TAttribute, TValue> valueSelector)
        where TAttribute : Attribute
    {
        if (type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() is TAttribute att)
        {
            return valueSelector(att);
        }

        return default;
    }

    public static TValue? GetPropertyAttributeValue<TAttribute, TValue>(
        this PropertyInfo property,
        Func<TAttribute, TValue> valueSelector)
        where TAttribute : Attribute
    {
        if (property.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() is TAttribute att)
        {
            return valueSelector(att);
        }

        return default;
    }
}
