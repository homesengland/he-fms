using System.ComponentModel.DataAnnotations;

namespace HE.FMS.Middleware.Contract.Reclaims;
public class CreateReclaimRequest
{
    [Required]
    [MaxLength(255)]
    public string ProductCode { get; set; }

    [Required]
    [Range(0.01, int.MaxValue)]
    public decimal NetAmount { get; set; }

    [Required]
    [MaxLength(255)]
    public string AllocationId { get; set; }

    [Required]
    [MaxLength(255)]
    public string ProviderId { get; set; }

    [Required]
    [MaxLength(255)]
    public string SchemeName { get; set; }

    [Required]
    [MaxLength(255)]
    public string MilestoneName { get; set; }
}
