namespace HE.FMS.IntegrationPlatform.Providers.KeyVault;

public interface IKeyVaultSecretClient
{
    Task<string> Get(string secretName, CancellationToken cancellationToken);

    Task<TSecret> Get<TSecret>(string secretName, CancellationToken cancellationToken);

    Task Set(string secretName, string secretValue, CancellationToken cancellationToken);

    Task Set<TSecret>(string secretName, TSecret secret, CancellationToken cancellationToken);
}
