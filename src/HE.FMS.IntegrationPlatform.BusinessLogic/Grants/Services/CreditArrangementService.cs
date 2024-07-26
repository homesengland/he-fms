using HE.FMS.IntegrationPlatform.Contract.Grants;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Enums;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement.Contract;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement.Contract.Enums;

namespace HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Services;

internal sealed class CreditArrangementService : ICreditArrangementService
{
    private readonly IMambuCreditArrangementApiClient _creditArrangementApiClient;

    public CreditArrangementService(IMambuCreditArrangementApiClient creditArrangementApiClient)
    {
        _creditArrangementApiClient = creditArrangementApiClient;
    }

    public async Task<CreditArrangementDto> GetOrCreateCreditArrangement(string applicationId, string groupId, GrantDetailsContract grantDetails, CancellationToken cancellationToken)
    {
        return await _creditArrangementApiClient.GetById(applicationId, DetailsLevel.Full, cancellationToken)
               ?? await _creditArrangementApiClient.Create(ToDto(applicationId, groupId, grantDetails), cancellationToken);
    }

    private static CreditArrangementDto ToDto(string applicationId, string groupId, GrantDetailsContract grantDetails)
    {
        return new CreditArrangementDto
        {
            Id = applicationId, // TODO: can we use applicationId as the CreditArrangement ID? If not how to perform deduplication?
            Amount = grantDetails.TotalFundingRequested,
            HolderKey = groupId,
            HolderType = HolderType.Group,
            StartDate = grantDetails.StartDate,
            ExpireDate = grantDetails.StartDate.AddYears(10), // TODO: This field is mandatory, but we don't have this information
            Notes = grantDetails.Notes,
        };
    }
}
