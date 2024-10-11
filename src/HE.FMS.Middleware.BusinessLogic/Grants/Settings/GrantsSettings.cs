using System.Diagnostics.CodeAnalysis;

namespace HE.FMS.Middleware.BusinessLogic.Grants.Settings;

[ExcludeFromCodeCoverage]
internal sealed class GrantsSettings : IGrantsSettings
{
    public string BranchId { get; set; }
}
