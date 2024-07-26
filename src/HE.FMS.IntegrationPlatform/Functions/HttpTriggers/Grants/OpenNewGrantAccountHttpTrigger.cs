using System.Net;
using HE.FMS.IntegrationPlatform.Common.Serialization;
using HE.FMS.IntegrationPlatform.Contract.Grants.Results;
using HE.FMS.IntegrationPlatform.Contract.Grants.UseCases;
using HE.FMS.IntegrationPlatform.Domain.Framework;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HE.FMS.IntegrationPlatform.Functions.HttpTriggers.Grants;

public class OpenNewGrantAccountHttpTrigger
{
    private readonly IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult> _useCase;

    private readonly IStreamSerializer _serializer;

    public OpenNewGrantAccountHttpTrigger(IUseCase<OpenNewGrantAccountRequest, OpenNewGrantAccountResult> useCase, IStreamSerializer serializer)
    {
        _useCase = useCase;
        _serializer = serializer;
    }

    [Function(nameof(OpenNewGrantAccountHttpTrigger))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "grants")]
        HttpRequestData request,
        FunctionContext executionContext,
        CancellationToken cancellationToken)
    {
        var dto = await _serializer.Deserialize<OpenNewGrantAccountRequest>(request.Body, cancellationToken);

        await _useCase.Trigger(dto, cancellationToken);

        return request.CreateResponse(HttpStatusCode.Created);
    }
}
