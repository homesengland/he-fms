namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Settings;

internal sealed class MambuApiKeySettings : IMambuApiKeySettings
{
    public int ExpirationInSeconds { get; set; }

    public string KeyVaultSecretName { get; set; }
}
