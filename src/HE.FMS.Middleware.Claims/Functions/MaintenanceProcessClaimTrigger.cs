using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using HE.FMS.Middleware.BusinessLogic.Trace.CosmosDb;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Providers.Common;
using HE.FMS.Middleware.Providers.SeriveBus.Settings;
using HE.FMS.Middleware.Shared.Base;
using HE.FMS.Middleware.Shared.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Claims.Functions;
public class MaintenanceProcessClaimTrigger : ClaimBase<ClaimPaymentRequest>
{
    private readonly ILogger<MaintenanceProcessClaimTrigger> _logger;

    public MaintenanceProcessClaimTrigger(
        IStreamSerializer streamSerializer,
        ITraceCosmosClient traceCosmosDbClient,
        IObjectSerializer objectSerializer,
        IAzureClientFactory<ServiceBusClient> clientFactory,
        IEnvironmentValidator environmentValidator,
        ServiceBusSettings serviceBusSettings,
        ILogger<MaintenanceProcessClaimTrigger> logger)
        : base(
            streamSerializer,
            traceCosmosDbClient,
            objectSerializer,
            clientFactory
                .CreateClient(Constants.Settings.ServiceBus.DefaultClientName)
                .CreateSender(serviceBusSettings.ClaimsTopic),
            environmentValidator)
    {
        _logger = logger;
    }

    [Function(nameof(MaintenanceProcessClaimTrigger))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "claims")]
        HttpRequestData request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"{nameof(MaintenanceProcessClaimTrigger)} function started");
            return await Trigger(request, cancellationToken);
        }
        catch (AggregateException)
        {
            throw new AggregateException(ConstantExceptionMessage.ClaimsValidationException);
        }
        finally
        {
            _logger.LogInformation($"{nameof(MaintenanceProcessClaimTrigger)} function ended");
        }
    }
}
