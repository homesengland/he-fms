using HE.FMS.Middleware.Contract.Mambu.Common.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.CreditArrangement;

public sealed class AddCreditArrangementAccountDto
{
    public string AccountId { get; set; }

    public AccountType AccountType { get; set; }
}
