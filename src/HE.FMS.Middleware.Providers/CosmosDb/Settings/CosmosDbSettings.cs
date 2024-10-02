using System.Diagnostics.CodeAnalysis;

namespace HE.FMS.Middleware.Providers.CosmosDb.Settings;

[ExcludeFromCodeCoverage]
public sealed class CosmosDbSettings
{
    public string AccountEndpoint { get; set; }

    public string ConnectionString { get; set; }

    public string DatabaseId { get; set; }

    public string ContainerId { get; set; }
}
