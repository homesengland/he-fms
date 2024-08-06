using System.ComponentModel.DataAnnotations;

namespace HE.FMS.Middleware.Contract.Grants.Results;

public sealed class OpenNewGrantAccountResult
{
    public OpenNewGrantAccountResult(string applicationId, string grantId, string phaseId)
    {
        ApplicationId = applicationId;
        GrantId = grantId;
        PhaseId = phaseId;
    }

    [Required]
    [MaxLength(32)]
    public string ApplicationId { get; }

    [Required]
    [MaxLength(32)]
    public string GrantId { get; }

    [Required]
    [MaxLength(32)]
    public string PhaseId { get; }
}
