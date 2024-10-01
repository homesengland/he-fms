using System.Diagnostics.CodeAnalysis;

namespace HE.FMS.Middleware.Providers.Mambu.Settings;

[ExcludeFromCodeCoverage]
internal sealed class MambuApiKeySettings : IMambuApiKeySettings
{
    public int ExpirationInSeconds { get; set; }

    public string KeyVaultValueName { get; set; }
}
