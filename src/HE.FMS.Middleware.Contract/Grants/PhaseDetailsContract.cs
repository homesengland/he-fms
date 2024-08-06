using System.ComponentModel.DataAnnotations;

namespace HE.FMS.Middleware.Contract.Grants;

public sealed class PhaseDetailsContract
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }

    [Required]
    [MinLength(1)]
    public IList<MilestoneContract> Milestones { get; set; } = [];
}
