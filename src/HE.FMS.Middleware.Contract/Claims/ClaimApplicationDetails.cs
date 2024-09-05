using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Claims;

public sealed class ClaimApplicationDetails
{
    [Required]
    [MaxLength(ValidatorConstants.IdMaxLength)]
    public string Id { get; set; }

    [Required]
    [MaxLength(ValidatorConstants.CustomNameMaxLength)]
    public string Name { get; set; }

    [Required]
    [MaxLength(ValidatorConstants.EnumMaxLength)]
    public string Region { get; set; }

    public string EfinRegion =>
        Region switch
        {
            nameof(EfinConstants.Region.North) => EfinConstants.Region.North,
            nameof(EfinConstants.Region.South) => EfinConstants.Region.South,
            nameof(EfinConstants.Region.Midlands) => EfinConstants.Region.Midlands,
            nameof(EfinConstants.Region.London) => EfinConstants.Region.London,
            _ => string.Empty,
        };

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
