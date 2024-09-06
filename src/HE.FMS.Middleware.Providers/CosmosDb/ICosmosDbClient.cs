using Microsoft.Azure.Cosmos;

namespace HE.FMS.Middleware.Providers.CosmosDb;

public interface ICosmosDbClient
{
    Task UpsertItem<TMessage>(TMessage message, CancellationToken cancellationToken)
        where TMessage : ICosmosDbItem;

    Task<List<TMessage>> GetItems<TMessage>(QueryDefinition definition, string partitionKey)
        where TMessage : ICosmosDbItem;
}
