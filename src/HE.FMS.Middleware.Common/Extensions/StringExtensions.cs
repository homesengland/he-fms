using System.Text;

namespace HE.FMS.Middleware.Common.Extensions;
public static class StringExtensions
{
    public static string RemoveSpecialCharacters(this string str)
    {
        var sb = new StringBuilder();
        foreach (var c in from c in str
                          where c is (>= '0' and <= '9') or (>= 'A' and <= 'Z') or (>= 'a' and <= 'z') or '.' or '_'
                          select c)
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

    public static string? TrimEnd(this string? value, int trimLength)
    {
        return value?.Length > trimLength
            ? value[..^trimLength]
            : value;
    }

    public static string ReplaceAt(this string value, int index, int length, string replace)
    {
        return value
            .Remove(index, Math.Min(length, value.Length - index))
            .Insert(index, replace);
    }

    public static string ReplaceAt(this string value, int index, int length, string replace, char gapFiller)
    {
#pragma warning disable IDE0057 // Use range operator
        return value
            .Remove(index, length)
            .Insert(index, replace.PadRight(length).Substring(0, length));
#pragma warning restore IDE0057 // Use range operator
    }
}
