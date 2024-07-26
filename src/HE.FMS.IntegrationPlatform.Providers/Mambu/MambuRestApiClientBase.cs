using System.Net;
using HE.FMS.IntegrationPlatform.Common.Exceptions.Communication;
using HE.FMS.IntegrationPlatform.Common.Extensions;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Contract;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Enums;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Extensions;
using Microsoft.Extensions.Logging;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu;

internal abstract class MambuRestApiClientBase<TDto, TGetAllParams> : MambuApiHttpClientBase, IMambuRestApiClient<TDto, TGetAllParams>
    where TDto : class
    where TGetAllParams : IGetAllParams
{
    protected MambuRestApiClientBase(HttpClient httpClient, ILogger<MambuRestApiClientBase<TDto, TGetAllParams>> logger)
        : base(httpClient, logger)
    {
    }

    protected abstract string ApiUrl { get; }

    public virtual async Task<IList<TDto>> Search(
        SearchCriteriaDto searchCriteria,
        PageDetails pageDetails,
        CancellationToken cancellationToken = default)
    {
        var relativeUrl = $"{ApiUrl}:search?{pageDetails.ToQueryString()}";
        return await Send<SearchCriteriaDto, IList<TDto>>(HttpMethod.Post, relativeUrl, searchCriteria, cancellationToken);
    }

    public virtual async Task<IList<TDto>> GetAll(
        TGetAllParams parameters,
        PageDetails pageDetails,
        CancellationToken cancellationToken)
    {
        var relativeUrl = $"{ApiUrl}?{pageDetails.ToQueryString(parameters)}";
        return await Send<IList<TDto>>(HttpMethod.Get, relativeUrl, cancellationToken);
    }

    public virtual async Task<TDto?> GetById(string id, DetailsLevel detailsLevel = DetailsLevel.Basic, CancellationToken cancellationToken = default)
    {
        try
        {
            return await Send<TDto>(HttpMethod.Get, $"{ApiUrl}/{id}?detailsLevel={detailsLevel.GetEnumDescription()}", cancellationToken);
        }
        catch (ExternalSystemCommunicationException ex) when (ex.Code == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public virtual async Task<TDto> Create(TDto item, CancellationToken cancellationToken)
    {
        return await Send<TDto, TDto>(HttpMethod.Post, ApiUrl, item, cancellationToken);
    }

    public virtual async Task<TDto> Update(TDto item, CancellationToken cancellationToken)
    {
        return await Send<TDto, TDto>(HttpMethod.Put, ApiUrl, item, cancellationToken);
    }

    public virtual async Task Delete(string id, CancellationToken cancellationToken)
    {
        await Send(HttpMethod.Delete, $"{ApiUrl}/{id}", cancellationToken);
    }
}
