using HE.FMS.IntegrationPlatform.Contract.Grants;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement.Contract;

namespace HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Services;

public interface ICreditArrangementService
{
    Task<CreditArrangementDto> GetOrCreateCreditArrangement(string applicationId, string groupId, GrantDetailsContract grantDetails, CancellationToken cancellationToken);
}
