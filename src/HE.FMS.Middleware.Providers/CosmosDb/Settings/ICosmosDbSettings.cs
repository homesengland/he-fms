namespace HE.FMS.Middleware.Providers.CosmosDb.Settings;

internal interface ICosmosDbSettings
{
    string? AccountEndpoint { get; }

    string? ConnectionString { get; }

    string DatabaseId { get; }

    string ContainerId { get; }
}
