﻿using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Contract;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Enums;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu;

public interface IMambuRestApiClient<in TDto, TReadDto, in TGetAllParams>
    where TDto : class
    where TReadDto : class, TDto
    where TGetAllParams : IGetAllParams
{
    Task<IList<TReadDto>> Search(
        SearchCriteriaDto searchCriteria,
        PageDetails pageDetails,
        CancellationToken cancellationToken = default);

    Task<IList<TReadDto>> GetAll(
        TGetAllParams parameters,
        PageDetails pageDetails,
        CancellationToken cancellationToken);

    Task<TReadDto?> GetById(string id, DetailsLevel detailsLevel = DetailsLevel.Basic, CancellationToken cancellationToken = default);

    Task<TReadDto> Create(TDto item, CancellationToken cancellationToken);

    Task<TReadDto> Update(TDto item, CancellationToken cancellationToken);

    Task Delete(string id, CancellationToken cancellationToken);
}
