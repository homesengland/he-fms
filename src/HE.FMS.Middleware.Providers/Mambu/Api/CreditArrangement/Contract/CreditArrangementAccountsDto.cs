using HE.FMS.Middleware.Providers.Mambu.Api.DepositAccount.Contract;
using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;

namespace HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement.Contract;

public sealed class CreditArrangementAccountsDto
{
    public IList<DepositAccountReadDto> DepositAccounts { get; set; }

    public IList<LoanAccountReadDto> LoanAccounts { get; set; }
}
