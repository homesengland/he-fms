using System.Diagnostics.CodeAnalysis;

namespace HE.FMS.Middleware.Providers.Mambu.Settings;

[ExcludeFromCodeCoverage]
internal sealed class MambuApiSettings : IMambuApiSettings
{
    public Uri BaseUrl { get; set; }

    public int RetryCount { get; set; }

    public int RetryDelayInMilliseconds { get; set; }
}
