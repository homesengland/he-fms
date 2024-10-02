using System.Diagnostics.CodeAnalysis;

namespace HE.FMS.Middleware.Providers.Common.Settings;

[ExcludeFromCodeCoverage]
public class MemoryCacheSettings
{
    public int TokenExpirationTimeInMinutes { get; set; }
}
