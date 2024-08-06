using HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement.Contract.Enums;

namespace HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement.Contract;

public sealed class CreditArrangementReadDto : CreditArrangementDto
{
    public DateTimeOffset? ApprovedDate { get; set; }

    public decimal AvailableCreditAmount { get; set; }

    public DateTimeOffset? ClosedDate { get; set; }

    public decimal ConsumedCreditAmount { get; set; }

    public DateTimeOffset CreationDate { get; set; }

    public string EncodedKey { get; set; }

    public DateTimeOffset? LastModifiedDate { get; set; }

    public CreditArrangementState State { get; set; }

    public CreditArrangementSubState SubState { get; set; }
}
