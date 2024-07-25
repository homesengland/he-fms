using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Contract;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Enums;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu;

public interface IMambuRestApiClient<TDto, in TGetAllParams>
    where TDto : class
    where TGetAllParams : IGetAllParams
{
    Task<IList<TDto>> Search(
        SearchCriteriaDto searchCriteria,
        PageDetails pageDetails,
        CancellationToken cancellationToken = default);

    Task<IList<TDto>> GetAll(
        TGetAllParams parameters,
        PageDetails pageDetails,
        CancellationToken cancellationToken);

    Task<TDto?> GetById(string id, DetailsLevel detailsLevel = DetailsLevel.Basic, CancellationToken cancellationToken = default);

    Task<TDto> Create(TDto item, CancellationToken cancellationToken);

    Task<TDto> Update(TDto item, CancellationToken cancellationToken);

    Task Delete(string id, CancellationToken cancellationToken);
}
