using System.ComponentModel.DataAnnotations;

namespace HE.FMS.IntegrationPlatform.Contract.Grants;

public class MilestoneContract
{
    [Required]
    public MilestoneType Type { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public DateTime MilestoneExpectedDisbursementDate { get; set; }
}
