using HE.FMS.Middleware.Contract.Mambu.DepositAccount;
using HE.FMS.Middleware.Contract.Mambu.LoanAccount;

namespace HE.FMS.Middleware.Contract.Mambu.CreditArrangement;

public sealed class CreditArrangementAccountsDto
{
    public IList<DepositAccountReadDto> DepositAccounts { get; set; }

    public IList<LoanAccountReadDto> LoanAccounts { get; set; }
}
