namespace HE.FMS.Middleware.Providers.Mambu.Settings;

internal sealed class MambuApiKeySettings : IMambuApiKeySettings
{
    public int ExpirationInSeconds { get; set; }

    public string KeyVaultSecretName { get; set; }
}
