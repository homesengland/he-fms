using System.Diagnostics.CodeAnalysis;

namespace HE.FMS.Middleware.Providers.Mambu.Settings;

[ExcludeFromCodeCoverage]
internal static class Consts
{
    public static readonly TimeZoneInfo UkTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/London");
}
