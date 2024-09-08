using HE.FMS.Middleware.Contract.Mambu.Common.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.DepositAccount;

public class DepositAccountReadDto
{
    public string Id { get; set; }

    public string AccountHolderKey { get; set; }

    public HolderType AccountHolderType { get; set; }
}
