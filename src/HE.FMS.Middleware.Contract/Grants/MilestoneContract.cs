using System.ComponentModel.DataAnnotations;

namespace HE.FMS.Middleware.Contract.Grants;

public class MilestoneContract
{
    [Required]
    public MilestoneType Type { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public DateTimeOffset? MilestoneExpectedDisbursementDate { get; set; }
}
