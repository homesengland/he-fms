using HE.FMS.Middleware.Providers.Mambu.Api.Common.Contract;
using HE.FMS.Middleware.Providers.Mambu.Api.Common.Enums;
using HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement.Contract.Enums;

namespace HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement.Contract;

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
