using System.Net;
using HE.FMS.Middleware.Common.Exceptions.Communication;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Providers.Mambu.Api.Common;
using HE.FMS.Middleware.Providers.Mambu.Api.Common.Contract;
using HE.FMS.Middleware.Providers.Mambu.Api.Common.Enums;
using HE.FMS.Middleware.Providers.Mambu.Extensions;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Providers.Mambu;

internal abstract class MambuRestApiClientBase<TDto, TReadDto, TGetAllParams> : MambuApiHttpClientBase, IMambuRestApiClient<TDto, TReadDto, TGetAllParams>
    where TDto : class
    where TReadDto : class, TDto
    where TGetAllParams : IGetAllParams
{
    protected MambuRestApiClientBase(HttpClient httpClient, ILogger<MambuRestApiClientBase<TDto, TReadDto, TGetAllParams>> logger)
        : base(httpClient, logger)
    {
    }

    protected abstract string ApiUrl { get; }

    public virtual async Task<IList<TReadDto>> Search(
        SearchCriteriaDto searchCriteria,
        PageDetails pageDetails,
        CancellationToken cancellationToken = default)
    {
        var relativeUrl = $"{ApiUrl}:search?{pageDetails.ToQueryString()}";
        return await Send<SearchCriteriaDto, IList<TReadDto>>(HttpMethod.Post, relativeUrl, searchCriteria, cancellationToken);
    }

    public virtual async Task<IList<TReadDto>> GetAll(
        TGetAllParams parameters,
        PageDetails pageDetails,
        CancellationToken cancellationToken)
    {
        var relativeUrl = $"{ApiUrl}?{pageDetails.ToQueryString(parameters)}";
        return await Send<IList<TReadDto>>(HttpMethod.Get, relativeUrl, cancellationToken);
    }

    public virtual async Task<TReadDto?> GetById(string id, DetailsLevel detailsLevel = DetailsLevel.Basic, CancellationToken cancellationToken = default)
    {
        try
        {
            return await Send<TReadDto>(HttpMethod.Get, $"{ApiUrl}/{id}?detailsLevel={detailsLevel.GetEnumDescription()}", cancellationToken);
        }
        catch (ExternalSystemCommunicationException ex) when (ex.Code == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public virtual async Task<TReadDto> Create(TDto item, CancellationToken cancellationToken)
    {
        return await Send<TDto, TReadDto>(HttpMethod.Post, ApiUrl, item, cancellationToken);
    }

    public virtual async Task<TReadDto> Update(TDto item, CancellationToken cancellationToken)
    {
        return await Send<TDto, TReadDto>(HttpMethod.Put, ApiUrl, item, cancellationToken);
    }

    public virtual async Task Delete(string id, CancellationToken cancellationToken)
    {
        await Send(HttpMethod.Delete, $"{ApiUrl}/{id}", cancellationToken);
    }
}
