using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group.Contract;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group;

public interface IMambuGroupApiClient
{
    Task<IList<GetGroupResponse>> GetAll(CancellationToken cancellationToken);
}
