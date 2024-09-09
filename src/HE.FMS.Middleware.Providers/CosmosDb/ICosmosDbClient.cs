using System.Linq.Expressions;

namespace HE.FMS.Middleware.Providers.CosmosDb;

public interface ICosmosDbClient
{
    Task UpsertItem<TMessage>(TMessage message, CancellationToken cancellationToken)
        where TMessage : ICosmosDbItem;

    Task<IList<TMessage>> FindAllItems<TMessage>(Expression<Func<TMessage, bool>> predicate, string partitionKey)
        where TMessage : ICosmosDbItem;

    Task UpdateFieldAsync<TMessage>(TMessage item, string fieldName, object fieldValue, string partitionKey)
        where TMessage : ICosmosDbItem;
}
