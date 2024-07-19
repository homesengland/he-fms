using HE.FMS.IntegrationPlatform.Contract.PoC;
using HE.FMS.IntegrationPlatform.Domain.PoC.Entities;
using HE.FMS.IntegrationPlatform.Providers.CosmosDb;

namespace HE.FMS.IntegrationPlatform.Domain.PoC;

internal sealed class PoCService : IPoCService
{
    private readonly ICosmosDbClient _cosmosDbClient;

    public PoCService(ICosmosDbClient cosmosDbClient)
    {
        _cosmosDbClient = cosmosDbClient;
    }

    public async Task PushToCosmosDb(string messageId, InputMessage message, CancellationToken cancellationToken)
    {
        var entity = new InputMessageEntity(messageId, "PoC", message.Name);

        await _cosmosDbClient.UpsertItem(entity, cancellationToken);
    }
}
