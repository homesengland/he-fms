namespace HE.FMS.Middleware.Providers.CosmosDb.Settings;

internal sealed class CosmosDbSettings : ICosmosDbSettings
{
    public string? AccountEndpoint { get; set; }

    public string? ConnectionString { get; set; }

    public string DatabaseId { get; set; }

    public string ContainerId { get; set; }
}
