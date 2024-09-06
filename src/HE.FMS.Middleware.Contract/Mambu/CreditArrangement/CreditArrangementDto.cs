using HE.FMS.Middleware.Contract.Mambu.Common.Contract;
using HE.FMS.Middleware.Contract.Mambu.Common.Enums;
using HE.FMS.Middleware.Contract.Mambu.CreditArrangement.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.CreditArrangement;

public class CreditArrangementDto
{
    public string Id { get; set; }

    public decimal Amount { get; set; }

    public CurrencyDto Currency { get; set; }

    public DateTimeOffset ExpireDate { get; set; }

    public ExposureLimitType? ExposureLimitType { get; set; }

    public string HolderKey { get; set; }

    public HolderType HolderType { get; set; }

    public string? Notes { get; set; }

    public DateTimeOffset StartDate { get; set; }
}
