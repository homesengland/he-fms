using HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Settings;
using HE.FMS.IntegrationPlatform.Contract.Grants;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Contract;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Enums;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract.Enums;

namespace HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Services;

internal sealed class LoanAccountService : ILoanAccountService
{
    private readonly IMambuLoanAccountApiClient _loanAccountApiClient;

    private readonly IGrantsSettings _grantsSettings;

    public LoanAccountService(IMambuLoanAccountApiClient loanAccountApiClient, IGrantsSettings grantsSettings)
    {
        _loanAccountApiClient = loanAccountApiClient;
        _grantsSettings = grantsSettings;
    }

    public async Task<LoanAccountReadDto> GetOrCreateLoanAccount(
        string creditArrangementId,
        string groupId,
        GrantDetailsContract grantDetails,
        PhaseDetailsContract phaseDetails,
        CancellationToken cancellationToken)
    {
        var searchCriteria = new SearchCriteriaDto
        {
            FilterCriteria = new[]
            {
                FilterCriterionDto.Equals("accountHolderKey", groupId),
                FilterCriterionDto.Equals("productTypeKey", grantDetails.ProductId),
                FilterCriterionDto.Equals("loanName", phaseDetails.Name),
            },
        };

        var searchResult = await _loanAccountApiClient.Search(searchCriteria, new PageDetails(), cancellationToken);
        if (searchResult.Any())
        {
            return searchResult[0];
        }

        return await _loanAccountApiClient.Create(ToDto(groupId, grantDetails, phaseDetails), cancellationToken);
    }

    private static LoanTrancheDto ToTrancheDto(MilestoneContract milestone)
    {
        return new LoanTrancheDto
        {
            Amount = milestone.Amount,
            DisbursementDetails = new TrancheDisbursementDetailsDto
            {
                ExpectedDisbursementDate = milestone.MilestoneExpectedDisbursementDate,
            },
        };
    }

    private LoanAccountDto ToDto(string groupId, GrantDetailsContract grantDetails, PhaseDetailsContract phaseDetails)
    {
        return new LoanAccountDto
        {
            AssignedBranchKey = _grantsSettings.BranchId,
            ProductTypeKey = grantDetails.ProductId,
            LoanName = phaseDetails.Name,
            Notes = phaseDetails.Notes,
            Tranches = phaseDetails.Milestones.Select(ToTrancheDto).ToList(),
            LoanAmount = phaseDetails.Amount,
            AccountHolderType = HolderType.Group,
            AccountHolderKey = groupId,
            ScheduleSettings = new ScheduleSettingsDto
            {
                GracePeriod = 0,
                GracePeriodType = GracePeriodType.None,
                RepaymentInstallments = 1,
                RepaymentPeriodCount = 100,
                RepaymentPeriodUnit = RepaymentPeriodUnit.Years,
                PrincipalRepaymentInterval = 1,
            },
            InterestSettings = new InterestSettingsDto
            {
                InterestRateSource = InterestRateSource.FixedInterestRate,
                InterestApplicationMethod = InterestApplicationMethod.RepaymentDueDate,
                InterestBalanceCalculationMethod = InterestBalanceCalculationMethod.OnlyPrincipal,
                InterestCalculationMethod = InterestCalculationMethod.DecliningBalance,
                InterestChargeFrequency = InterestChargeFrequency.Annualized,
                InterestRate = 0,
                InterestType = InterestType.SimpleInterest,
            },
        };
    }
}
