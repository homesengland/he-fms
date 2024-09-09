using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HE.FMS.Middleware.Common.Exceptions.Validation;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Providers.CosmosDb;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.Middleware.Shared.Base;
public class ClaimBase<T>
{
    private readonly IStreamSerializer _streamSerializer;
    private readonly ICosmosDbClient _cosmosDbClient;

    protected ClaimBase(
        IStreamSerializer streamSerializer,
        ICosmosDbClient cosmosDbClient)
    {
        _streamSerializer = streamSerializer;
        _cosmosDbClient = cosmosDbClient;
    }

    protected async Task<HttpResponseData> Trigger(HttpRequestData request, CosmosDbItemType type, CancellationToken cancellationToken)
    {
        var inputData = await _streamSerializer.Deserialize<T>(request.Body, cancellationToken) ?? throw new InvalidRequestException($"Empty request body");

        var idempotencyKey = request.GetIdempotencyHeader();

        var cosmosDbOutput = CosmosDbItem.CreateCosmosDbItem(inputData, idempotencyKey, type);

        await _cosmosDbClient.UpsertItem(cosmosDbOutput, cancellationToken);

        return request.CreateResponse(HttpStatusCode.Accepted);
    }
}
