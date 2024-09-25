using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Serialization;
using HE.FMS.Middleware.Contract.Claims;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Constants;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Providers.Common;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HE.FMS.Middleware.Claims.Functions;
public class TransformClaimServiceBusTrigger
{
    private readonly IStreamSerializer _streamSerializer;
    private readonly IClaimConverter _claimConverter;
    private readonly IEfinCosmosClient _efinCosmosDbClient;
    private readonly IEnvironmentValidator _environmentValidator;
    private readonly ILogger<TransformClaimServiceBusTrigger> _logger;

    public TransformClaimServiceBusTrigger(
        IStreamSerializer streamSerializer,
        IClaimConverter claimConverter,
        IEfinCosmosClient efinCosmosDbClient,
        IEnvironmentValidator environmentValidator,
        ILogger<TransformClaimServiceBusTrigger> logger)
    {
        _streamSerializer = streamSerializer;
        _claimConverter = claimConverter;
        _efinCosmosDbClient = efinCosmosDbClient;
        _environmentValidator = environmentValidator;
        _logger = logger;
    }

    [Function(nameof(TransformClaimServiceBusTrigger))]
    public async Task Run(
        [ServiceBusTrigger("%Claims:Create:TopicName%", "%Claims:Create:SubscriptionName%", Connection = "ServiceBus:Connection")]
        ServiceBusReceivedMessage message,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(TransformClaimServiceBusTrigger)} function started");

        var environment = message.ApplicationProperties[Constants.CustomHeaders.Environment]?.ToString();

        _environmentValidator.Validate(environment);

        var inputData = await _streamSerializer.Deserialize<ClaimPaymentRequest>(message.Body.ToStream(), cancellationToken);
        var convertedData = await _claimConverter.CreateItems(inputData);
        var cosmosDbOutput = EfinItem.CreateEfinItem(Constants.CosmosDbConfiguration.PartitonKey, convertedData, message.CorrelationId, environment!, CosmosDbItemType.Claim);
        await _efinCosmosDbClient.UpsertItem(cosmosDbOutput, cancellationToken);

        _logger.LogInformation($"{nameof(TransformClaimServiceBusTrigger)} function ended");
    }
}
