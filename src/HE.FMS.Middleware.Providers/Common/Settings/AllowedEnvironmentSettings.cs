using System.Diagnostics.CodeAnalysis;

namespace HE.FMS.Middleware.Providers.Common.Settings;

[ExcludeFromCodeCoverage]
public class AllowedEnvironmentSettings(string environments)
{
    public string[] Environments { get; set; } = environments.Split(',');
}
