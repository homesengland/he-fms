using System.Diagnostics.CodeAnalysis;
using HE.FMS.Middleware.Contract.Grants;
using HE.FMS.Middleware.Contract.Mambu.Common.Enums;
using HE.FMS.Middleware.Contract.Mambu.CreditArrangement;
using HE.FMS.Middleware.Contract.Mambu.LoanAccount;
using HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement;

namespace HE.FMS.Middleware.BusinessLogic.Grants.Services;

[ExcludeFromCodeCoverage]
public sealed class CreditArrangementService : ICreditArrangementService
{
    private static readonly DateTimeOffset CreditArrangementExpireDate = new(2199, 12, 31, 23, 59, 59, TimeSpan.Zero);

    private readonly IMambuCreditArrangementApiClient _creditArrangementApiClient;

    public CreditArrangementService(IMambuCreditArrangementApiClient creditArrangementApiClient)
    {
        _creditArrangementApiClient = creditArrangementApiClient;
    }

    public async Task<CreditArrangementReadDto> GetOrCreateCreditArrangement(string applicationId, string groupId, GrantDetailsContract grantDetails, CancellationToken cancellationToken)
    {
        return await _creditArrangementApiClient.GetById(applicationId, DetailsLevel.Full, cancellationToken)
               ?? await _creditArrangementApiClient.Create(ToDto(applicationId, groupId, grantDetails), cancellationToken);
    }

    public async Task<LoanAccountReadDto> AddLoanAccount(string creditArrangementId, string loanAccountId, CancellationToken cancellationToken)
    {
        var accounts = await _creditArrangementApiClient.AddAccount(creditArrangementId, loanAccountId, AccountType.Loan, cancellationToken);

        return accounts.LoanAccounts.First(x => x.EncodedKey == loanAccountId);
    }

    private static CreditArrangementDto ToDto(string applicationId, string groupId, GrantDetailsContract grantDetails)
    {
        return new CreditArrangementDto
        {
            Id = applicationId,
            Amount = grantDetails.TotalFundingRequested,
            HolderKey = groupId,
            HolderType = HolderType.Group,
            StartDate = grantDetails.StartDate,
            ExpireDate = CreditArrangementExpireDate,
            Notes = grantDetails.Notes,
        };
    }
}
