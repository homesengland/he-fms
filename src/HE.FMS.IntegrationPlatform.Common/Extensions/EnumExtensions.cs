using System.ComponentModel;
using System.Reflection;

namespace HE.FMS.IntegrationPlatform.Common.Extensions;

public static class EnumExtensions
{
    public static string GetEnumDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

        return attribute == null ? value.ToString() : attribute.Description;
    }

    public static TEnum? GetEnumValue<TEnum>(this string description)
        where TEnum : struct, Enum
    {
        var result = GetDescriptions<TEnum>().FirstOrDefault(x => x.Value == description || x.Key.ToString() == description);
        return result.Value == default ? null : result.Key;
    }

    private static IEnumerable<KeyValuePair<TEnum, string>> GetDescriptions<TEnum>()
        where TEnum : struct, Enum
    {
        foreach (TEnum value in Enum.GetValuesAsUnderlyingType<TEnum>())
        {
            yield return new KeyValuePair<TEnum, string>(value, value.GetEnumDescription());
        }
    }
}
