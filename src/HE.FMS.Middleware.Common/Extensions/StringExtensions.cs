using System.Text;

namespace HE.FMS.Middleware.Common.Extensions;
public static class StringExtensions
{
    public static string RemoveSpecialCharacters(this string str)
    {
        var sb = new StringBuilder();
#pragma warning disable IDE0055
        foreach (var c in from c in str
                          where c is (>= '0' and <= '9') or (>= 'A' and <= 'Z') or (>= 'a' and <= 'z') or '.' or '_'
                          select c)
#pragma warning restore IDE0055
        {
            sb.Append(c);
        }

        return sb.ToString();
    }

    public static string? Truncate(this string? value, int maxLength, string truncationSuffix = "")
    {
        return value?.Length > maxLength
            ? value[..maxLength] + truncationSuffix
            : value;
    }
}
