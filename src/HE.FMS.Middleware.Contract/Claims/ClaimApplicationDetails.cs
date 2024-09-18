using System.ComponentModel.DataAnnotations;
using System.Globalization;
using HE.FMS.Middleware.Common.Extensions;
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
    [MaxLength(ValidatorConstants.CustomNameMaxLength)]
    public string AllocationId { get; set; }

    [Required]
    [MaxLength(ValidatorConstants.EnumMaxLength)]
    public string Region { get; set; }

    [Required]
    [MaxLength(ValidatorConstants.EnumMaxLength)]
    public string Tenure { get; set; }

    public string EfinTenure =>
        Tenure.RemoveSpecialCharacters() switch
        {
            nameof(EfinConstants.Tenure.AffordableRent) => EfinConstants.Tenure.AffordableRent.ToString(CultureInfo.InvariantCulture),
            nameof(EfinConstants.Tenure.SocialRent) => EfinConstants.Tenure.SocialRent.ToString(CultureInfo.InvariantCulture),
            nameof(EfinConstants.Tenure.SharedOwnership) => EfinConstants.Tenure.SharedOwnership.ToString(CultureInfo.InvariantCulture),
            nameof(EfinConstants.Tenure.RentToBuy) => EfinConstants.Tenure.RentToBuy.ToString(CultureInfo.InvariantCulture),
            nameof(EfinConstants.Tenure.HOLD) => EfinConstants.Tenure.HOLD.ToString(CultureInfo.InvariantCulture),
            nameof(EfinConstants.Tenure.OPSO) => EfinConstants.Tenure.OPSO.ToString(CultureInfo.InvariantCulture),
            _ => string.Empty,
        };

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
