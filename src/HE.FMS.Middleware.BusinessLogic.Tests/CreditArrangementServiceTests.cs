using HE.FMS.Middleware.BusinessLogic.Grants.Services;
using HE.FMS.Middleware.Contract.Grants;
using HE.FMS.Middleware.Providers.Mambu.Api.Common.Enums;
using HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement;
using HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement.Contract;
using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests;

public class CreditArrangementServiceTests
{
    private readonly IMambuCreditArrangementApiClient _creditArrangementApiClient;
    private readonly CreditArrangementService _creditArrangementService;

    public CreditArrangementServiceTests()
    {
        _creditArrangementApiClient = Substitute.For<IMambuCreditArrangementApiClient>();
        _creditArrangementService = new CreditArrangementService(_creditArrangementApiClient);
    }

    [Fact]
    public async Task GetOrCreateCreditArrangement_ShouldReturnExistingArrangement_WhenArrangementExists()
    {
        // Arrange
        var applicationId = "app123";
        var groupId = "group123";
        var grantDetails = new GrantDetailsContract();
        var cancellationToken = CancellationToken.None;
        var existingArrangement = new CreditArrangementReadDto();

        _creditArrangementApiClient
            .GetById(applicationId, DetailsLevel.Full, cancellationToken)
            .Returns(existingArrangement);

        // Act
        var result = await _creditArrangementService.GetOrCreateCreditArrangement(applicationId, groupId, grantDetails, cancellationToken);

        // Assert
        Assert.Equal(existingArrangement, result);
        await _creditArrangementApiClient.Received(1).GetById(applicationId, DetailsLevel.Full, cancellationToken);
        await _creditArrangementApiClient.DidNotReceive().Create(Arg.Any<CreditArrangementDto>(), cancellationToken);
    }

    [Fact]
    public async Task GetOrCreateCreditArrangement_ShouldCreateNewArrangement_WhenArrangementDoesNotExist()
    {
        // Arrange
        var applicationId = "app123";
        var groupId = "group123";
        var grantDetails = new GrantDetailsContract();
        var cancellationToken = CancellationToken.None;
        var newArrangement = new CreditArrangementReadDto();

        _creditArrangementApiClient
            .GetById(applicationId, DetailsLevel.Full, cancellationToken)
            .Returns((CreditArrangementReadDto)null!);

        _creditArrangementApiClient
            .Create(Arg.Any<CreditArrangementDto>(), cancellationToken)
            .Returns(newArrangement);

        // Act
        var result = await _creditArrangementService.GetOrCreateCreditArrangement(applicationId, groupId, grantDetails, cancellationToken);

        // Assert
        Assert.Equal(newArrangement, result);
        await _creditArrangementApiClient.Received(1).GetById(applicationId, DetailsLevel.Full, cancellationToken);
        await _creditArrangementApiClient.Received(1).Create(Arg.Any<CreditArrangementDto>(), cancellationToken);
    }

    [Fact]
    public async Task AddLoanAccount_ShouldReturnLoanAccount_WhenAccountIsAdded()
    {
        // Arrange
        var creditArrangementId = "credit123";
        var loanAccountId = "loan123";
        var cancellationToken = CancellationToken.None;
        var loanAccount = new LoanAccountReadDto { EncodedKey = loanAccountId };
        var accounts = new CreditArrangementAccountsDto
        {
#pragma warning disable IDE0300
            LoanAccounts = new[] { loanAccount },
#pragma warning restore IDE0300
        };

        _creditArrangementApiClient
            .AddAccount(creditArrangementId, loanAccountId, AccountType.Loan, cancellationToken)
            .Returns(accounts);

        // Act
        var result = await _creditArrangementService.AddLoanAccount(creditArrangementId, loanAccountId, cancellationToken);

        // Assert
        Assert.Equal(loanAccount, result);
        await _creditArrangementApiClient.Received(1).AddAccount(creditArrangementId, loanAccountId, AccountType.Loan, cancellationToken);
    }
}
