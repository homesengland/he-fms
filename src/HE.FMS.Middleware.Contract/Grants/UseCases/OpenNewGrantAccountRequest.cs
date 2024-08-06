using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Common;

namespace HE.FMS.Middleware.Contract.Grants.UseCases;

public sealed class OpenNewGrantAccountRequest
{
    [Required]
    public OrganisationContract Organisation { get; set; }

    [Required]
    [MaxLength(255)]
    public string ApplicationId { get; set; }

    [Required]
    public GrantDetailsContract GrantDetails { get; set; }

    [Required]
    public PhaseDetailsContract PhaseDetails { get; set; }
}
