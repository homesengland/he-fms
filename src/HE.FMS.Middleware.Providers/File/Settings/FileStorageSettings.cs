using System.Diagnostics.CodeAnalysis;

namespace HE.FMS.Middleware.Providers.File.Settings;

[ExcludeFromCodeCoverage]
public sealed class FileStorageSettings
{
    public string ConnectionString { get; set; }

    public string ShareName { get; set; }
}
