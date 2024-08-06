using System.ComponentModel.DataAnnotations;

namespace HE.FMS.Middleware.Contract.Grants;

public sealed class GrantDetailsContract
{
    [Required]
    [MaxLength(32)]
    public string ProductId { get; set; }

    [Required]
    public decimal TotalFundingRequested { get; set; }

    [Required]
    public DateTimeOffset StartDate { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }
}
