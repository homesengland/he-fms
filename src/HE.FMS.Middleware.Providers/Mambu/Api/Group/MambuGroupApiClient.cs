using System.Diagnostics.CodeAnalysis;
using HE.FMS.Middleware.Contract.Mambu.Group;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Providers.Mambu.Api.Group;

[ExcludeFromCodeCoverage]
internal sealed class MambuGroupApiClient : MambuRestApiClientBase<GroupDto, GroupReadDto, GetAllGroupsParams>, IMambuGroupApiClient
{
    public MambuGroupApiClient(HttpClient httpClient, ILogger<MambuGroupApiClient> logger)
        : base(httpClient, logger)
    {
    }

    protected override string ApiUrl => "/api/groups";

    protected override string ApiName => "Mambu.GroupApi";
}
