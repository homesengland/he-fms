namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Settings;

public interface IMambuApiKeySettings
{
    int ExpirationInSeconds { get; }

    string KeyVaultSecretName { get; }
}
