namespace HE.FMS.Middleware.Providers.Mambu.Settings;

public interface IMambuApiKeySettings
{
    int ExpirationInSeconds { get; }

    string KeyVaultSecretName { get; }
}
