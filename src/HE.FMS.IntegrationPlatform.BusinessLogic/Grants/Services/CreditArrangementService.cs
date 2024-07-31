using HE.FMS.IntegrationPlatform.Contract.Grants;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Enums;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement.Contract;

namespace HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Services;

internal sealed class CreditArrangementService : ICreditArrangementService
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
