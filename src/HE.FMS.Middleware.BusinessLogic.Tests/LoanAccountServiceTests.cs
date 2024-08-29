using HE.FMS.Middleware.BusinessLogic.Grants.Services;
using HE.FMS.Middleware.BusinessLogic.Grants.Settings;
using HE.FMS.Middleware.Contract.Grants;
using HE.FMS.Middleware.Providers.Mambu.Api.Common;
using HE.FMS.Middleware.Providers.Mambu.Api.Common.Contract;
using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount;
using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests;

public class LoanAccountServiceTests
{
    private readonly IMambuLoanAccountApiClient _loanAccountApiClient;
    private readonly IGrantsSettings _grantsSettings;
    private readonly LoanAccountService _loanAccountService;

    public LoanAccountServiceTests()
    {
        _loanAccountApiClient = Substitute.For<IMambuLoanAccountApiClient>();
        _grantsSettings = Substitute.For<IGrantsSettings>();
        _loanAccountService = new LoanAccountService(_loanAccountApiClient, _grantsSettings);
    }

    [Fact]
    public async Task GetOrCreateLoanAccount_ShouldReturnExistingAccount_WhenAccountExists()
    {
        // Arrange
        var creditArrangementId = "creditArrangementId";
        var groupId = "groupId";
        var grantDetails = new GrantDetailsContract { ProductId = "productId" };
        var phaseDetails = new PhaseDetailsContract { Name = "phaseName" };
        var cancellationToken = CancellationToken.None;

        var existingAccount = new LoanAccountReadDto();
        _loanAccountApiClient.Search(Arg.Any<SearchCriteriaDto>(), Arg.Any<PageDetails>(), cancellationToken)
            .Returns(new List<LoanAccountReadDto> { existingAccount });

        // Act
        var (account, accountAlreadyExists) = await _loanAccountService.GetOrCreateLoanAccount(creditArrangementId, groupId, grantDetails, phaseDetails, cancellationToken);

        // Assert
        Assert.True(accountAlreadyExists);
        Assert.Equal(existingAccount, account);
    }

    [Fact]
    public async Task GetOrCreateLoanAccount_ShouldCreateNewAccount_WhenAccountDoesNotExist()
    {
        // Arrange
        var creditArrangementId = "creditArrangementId";
        var groupId = "groupId";
        var grantDetails = new GrantDetailsContract { ProductId = "productId" };
        var phaseDetails = new PhaseDetailsContract { Name = "phaseName" };
        var cancellationToken = CancellationToken.None;

        _loanAccountApiClient.Search(Arg.Any<SearchCriteriaDto>(), Arg.Any<PageDetails>(), cancellationToken)
            .Returns(new List<LoanAccountReadDto>());

        var newAccount = new LoanAccountReadDto();
        _loanAccountApiClient.Create(Arg.Any<LoanAccountDto>(), cancellationToken).Returns(newAccount);

        // Act
        var result = await _loanAccountService.GetOrCreateLoanAccount(creditArrangementId, groupId, grantDetails, phaseDetails, cancellationToken);

        // Assert
        Assert.False(result.AccountAlreadyExists);
        Assert.Equal(newAccount, result.Account);
    }
}
