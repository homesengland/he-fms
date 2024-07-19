namespace HE.FMS.IntegrationPlatform.Providers.CosmosDb.Settings;

public interface ICosmosDbSettings
{
    string? AccountEndpoint { get; }

    string? ConnectionString { get; }

    string DatabaseId { get; }

    string ContainerId { get; }
}
