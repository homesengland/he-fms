using System.ComponentModel.DataAnnotations;

namespace HE.FMS.Middleware.Contract.Claims;

public sealed class ClaimApplicationDetails
{
    [Required]
    [MaxLength(Constants.ValidatorConstants.IdMaxLength)]
    public string Id { get; set; }

    [Required]
    [MaxLength(Constants.ValidatorConstants.CustomNameMaxLength)]
    public string Name { get; set; }

    [Required]
    [MaxLength(Constants.ValidatorConstants.EnumMaxLength)]
    public string Region { get; set; }

    [Required]
    [MaxLength(Constants.ValidatorConstants.EnumMaxLength)]
    public string Tenure { get; set; }

    [Required]
    [MaxLength(Constants.ValidatorConstants.EnumMaxLength)]
    public string RevenueIndicator { get; set; }

    [Required]
    [MaxLength(Constants.ValidatorConstants.EnumMaxLength)]
    public string VatCode { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public decimal VatRate { get; set; }
}
