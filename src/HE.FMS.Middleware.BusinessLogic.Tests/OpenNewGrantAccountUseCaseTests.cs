using HE.FMS.Middleware.BusinessLogic.Grants;
using HE.FMS.Middleware.BusinessLogic.Grants.Services;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Contract.Grants;
using HE.FMS.Middleware.Contract.Grants.UseCases;
using HE.FMS.Middleware.Providers.Mambu.Api.CreditArrangement.Contract;
using HE.FMS.Middleware.Providers.Mambu.Api.Group.Contract;
using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract;
using HE.FMS.Middleware.Providers.Mambu.Api.LoanAccount.Contract.Enums;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests;

public class OpenNewGrantAccountUseCaseTests
{
    private readonly IGroupService _groupService;
    private readonly ICreditArrangementService _creditArrangementService;
    private readonly ILoanAccountService _loanAccountService;
    private readonly OpenNewGrantAccountUseCase _useCase;

    public OpenNewGrantAccountUseCaseTests()
    {
        _groupService = Substitute.For<IGroupService>();
        _creditArrangementService = Substitute.For<ICreditArrangementService>();
        _loanAccountService = Substitute.For<ILoanAccountService>();
        _useCase = new OpenNewGrantAccountUseCase(_groupService, _creditArrangementService, _loanAccountService);
    }

    [Fact]
    public async Task Trigger_ShouldReturnExpectedResult_WhenAccountAlreadyExists()
    {
        // Arrange
        var request = new OpenNewGrantAccountRequest
        {
            ApplicationId = "app123",
            Organisation = new OrganisationContract { Id = "org123" },
            GrantDetails = new GrantDetailsContract(),
            PhaseDetails = new PhaseDetailsContract(),
        };
        var cancellationToken = CancellationToken.None;

        var group = new GroupReadDto { EncodedKey = "groupKey" };
        var creditArrangement = new CreditArrangementReadDto { EncodedKey = "creditKey", Id = "creditId" };
        var loanAccount = new LoanAccountReadDto
        {
            EncodedKey = "loanKey",
            Id = "loanId",
            LoanName = "loanName",
            CreationDate = DateTime.UtcNow,
            AccountState = AccountState.Active,
        };

        _groupService.GetOrCreateGroup(request.Organisation, cancellationToken).Returns(Task.FromResult(group));
        _creditArrangementService.GetOrCreateCreditArrangement(request.ApplicationId, group.EncodedKey, request.GrantDetails, cancellationToken).Returns(Task.FromResult(creditArrangement));
        _loanAccountService.GetOrCreateLoanAccount(creditArrangement.EncodedKey, group.EncodedKey, request.GrantDetails, request.PhaseDetails, cancellationToken).Returns(Task.FromResult((loanAccount, true)));

        // Act
        var result = await _useCase.Trigger(request, cancellationToken);

        // Assert
        Assert.Equal(request.ApplicationId, result.ApplicationId);
        Assert.Equal(creditArrangement.Id, result.CreditArrangementId);
        Assert.Equal(loanAccount.Id, result.LoanId);
        Assert.Equal(loanAccount.LoanName, result.LoanName);
        Assert.Equal(loanAccount.CreationDate, result.LoanCreationDate);
        Assert.Equal(loanAccount.AccountState, result.LoanAccountState);
    }

    [Fact]
    public async Task Trigger_ShouldAddLoanAccount_WhenAccountDoesNotExist()
    {
        // Arrange
        var request = new OpenNewGrantAccountRequest
        {
            ApplicationId = "app123",
            Organisation = new OrganisationContract { Id = "org123" },
            GrantDetails = new GrantDetailsContract(),
            PhaseDetails = new PhaseDetailsContract(),
        };
        var cancellationToken = CancellationToken.None;

        var group = new GroupReadDto() { EncodedKey = "groupKey" };
        var creditArrangement = new CreditArrangementReadDto() { EncodedKey = "creditKey", Id = "creditId" };
        var loanAccount = new LoanAccountReadDto() { EncodedKey = "loanKey", Id = "loanId", LoanName = "loanName", CreationDate = DateTime.UtcNow, AccountState = AccountState.Active };

        _groupService.GetOrCreateGroup(request.Organisation, cancellationToken).Returns(Task.FromResult(group));
        _creditArrangementService.GetOrCreateCreditArrangement(request.ApplicationId, group.EncodedKey, request.GrantDetails, cancellationToken).Returns(Task.FromResult(creditArrangement));
        _loanAccountService.GetOrCreateLoanAccount(creditArrangement.EncodedKey, group.EncodedKey, request.GrantDetails, request.PhaseDetails, cancellationToken).Returns(Task.FromResult((loanAccount, false)));
        _creditArrangementService.AddLoanAccount(creditArrangement.EncodedKey, loanAccount.EncodedKey, cancellationToken).Returns(Task.FromResult(loanAccount));

        // Act
        var result = await _useCase.Trigger(request, cancellationToken);

        // Assert
        Assert.Equal(request.ApplicationId, result.ApplicationId);
        Assert.Equal(creditArrangement.Id, result.CreditArrangementId);
        Assert.Equal(loanAccount.Id, result.LoanId);
        Assert.Equal(loanAccount.LoanName, result.LoanName);
        Assert.Equal(loanAccount.CreationDate, result.LoanCreationDate);
        Assert.Equal(loanAccount.AccountState, result.LoanAccountState);
    }
}
