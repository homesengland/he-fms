using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using Azure.Messaging.ServiceBus;
using HE.FMS.Middleware.BusinessLogic.Trace.CosmosDb;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Exceptions.Validation;
using HE.FMS.Middleware.Common.Extensions;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Providers.Common;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.Middleware.Shared.Base;

[ExcludeFromCodeCoverage]
public abstract class ClaimBase<T>
{
    private readonly IStreamSerializer _streamSerializer;
    private readonly ITraceCosmosClient _traceCosmosDbClient;
    private readonly ServiceBusSender _serviceBusSender;
    private readonly IObjectSerializer _objectSerializer;
    private readonly IEnvironmentValidator _environmentValidator;

    protected ClaimBase(
        IStreamSerializer streamSerializer,
        ITraceCosmosClient traceComsmosDbClient,
        IObjectSerializer objectSerializer,
        ServiceBusSender serviceBusSender,
        IEnvironmentValidator environmentValidator)
    {
        _streamSerializer = streamSerializer;
        _traceCosmosDbClient = traceComsmosDbClient;
        _objectSerializer = objectSerializer;
        _serviceBusSender = serviceBusSender;
        _environmentValidator = environmentValidator;
    }

    protected async Task<HttpResponseData> Trigger(HttpRequestData request, CosmosDbItemType type, CancellationToken cancellationToken)
    {
        var environment = request.GetEnvironmentHeader();

        _environmentValidator.Validate(environment);

        var idempotencyKey = request.GetIdempotencyHeader();

        var inputData = await _streamSerializer.Deserialize<T>(request.Body, cancellationToken) ?? throw new InvalidRequestException($"Empty request body");

        var cosmosDbOutput = TraceItem.CreateTraceItem(Constants.CosmosDbConfiguration.PartitonKey, inputData, idempotencyKey, environment, type);

        await _traceCosmosDbClient.UpsertItem(cosmosDbOutput, cancellationToken);

        ServiceBusMessage message = new(Encoding.UTF8.GetBytes(_objectSerializer.Serialize(inputData)))
        {
            CorrelationId = idempotencyKey,
        };

        message.ApplicationProperties.Add(Constants.CustomHeaders.Environment, environment);

        await _serviceBusSender.SendMessageAsync(message, cancellationToken);

        return request.CreateResponse(HttpStatusCode.Accepted);
    }
}
