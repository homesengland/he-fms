using System.ComponentModel.DataAnnotations;
using HE.FMS.Middleware.Contract.Mambu.LoanAccount.Enums;

namespace HE.FMS.Middleware.Contract.Grants.Results;

public sealed class OpenNewGrantAccountResult
{
    public OpenNewGrantAccountResult(
        string applicationId,
        string creditArrangementId,
        string loanTransactionId,
        string loanTransactionName,
        DateTimeOffset loanCreationDate,
        AccountState? loanAccountState)
    {
        ApplicationId = applicationId;
        CreditArrangementId = creditArrangementId;
        LoanId = loanTransactionId;
        LoanName = loanTransactionName;
        LoanCreationDate = loanCreationDate;
        LoanAccountState = loanAccountState;
    }

    [Required]
    [MaxLength(32)]
    public string ApplicationId { get; }

    [Required]
    [MaxLength(32)]
    public string CreditArrangementId { get; }

    [Required]
    [MaxLength(32)]
    public string LoanId { get; }

    [Required]
    [MaxLength(255)]
    public string LoanName { get; }

    [Required]
    public DateTimeOffset LoanCreationDate { get; }

    [Required]
    public AccountState? LoanAccountState { get; }
}
