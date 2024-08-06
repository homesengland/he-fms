using HE.FMS.Middleware.BusinessLogic.PoC.Entities;
using HE.FMS.Middleware.Contract.PoC;
using HE.FMS.Middleware.Providers.CosmosDb;

namespace HE.FMS.Middleware.BusinessLogic.PoC;

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
