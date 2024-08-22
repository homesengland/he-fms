using System.ComponentModel.DataAnnotations;

namespace HE.FMS.Middleware.Contract.Claims;
public class CreateClaimRequest
{
    [Required]
    [Range(0.01, int.MaxValue)]
    public decimal ClaimAmount { get; set; }

    [Required]
    [MaxLength(255)]
    public string ClaimPaymentReference { get; set; }

    [Required]
    [MaxLength(255)]
    public string SupplierCode { get; set; }

    [Required]
    [MaxLength(255)]
    public string CostCentre { get; set; }

    [Required]
    [MaxLength(255)]
    public string ProductCode { get; set; }

    [Required]
    [MaxLength(255)]
    public string AuthorisedBy { get; set; }

    [Required]
    public DateTimeOffset AuthorisedOn { get; set; }
}
