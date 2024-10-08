using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Constants;
using HE.FMS.Middleware.Contract.Enums;

namespace HE.FMS.Middleware.Contract.Claims;
public sealed class ClaimApplicationDetails
{
    [Required]
    [MaxLength(ValidatorConstants.IdMaxLength)]
    public string ApplicationId { get; set; }

    [Required]
    [MaxLength(ValidatorConstants.CustomNameMaxLength)]
    public string SchemaName { get; set; }

    [Required]
    [MaxLength(ValidatorConstants.CustomNameMaxLength)]
    public string AllocationId { get; set; }

    [Required]
    public Region Region { get; set; }

    [Required]
    public Tenure Tenure { get; set; }

    [Required]
    public RevenueIndicator RevenueIndicator { get; set; }

    [Required]
    public VatCode VatCode { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public decimal VatRate { get; set; }
}
