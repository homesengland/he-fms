using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Constants;

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
    [MaxLength(ValidatorConstants.EnumMaxLength)]
    public string Region { get; set; }

    [Required]
    [MaxLength(ValidatorConstants.EnumMaxLength)]
    public string Tenure { get; set; }

    [Required]
    [MaxLength(ValidatorConstants.EnumMaxLength)]
    public string RevenueIndicator { get; set; }

    [Required]
    [MaxLength(ValidatorConstants.EnumMaxLength)]
    public string VatCode { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public decimal VatRate { get; set; }
}
