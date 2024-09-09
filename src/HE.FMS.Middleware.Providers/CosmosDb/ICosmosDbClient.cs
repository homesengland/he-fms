using System.Linq.Expressions;
using HE.FMS.Middleware.Providers.CosmosDb.Base;

namespace HE.FMS.Middleware.Providers.CosmosDb;

public interface ICosmosDbClient
{
    Task UpsertItem<TMessage>(TMessage message, CancellationToken cancellationToken)
        where TMessage : IDbItem;

    Task<IList<TMessage>> FindAllItems<TMessage>(Expression<Func<TMessage, bool>> predicate, string partitionKey)
        where TMessage : IDbItem;

    Task UpdateFieldAsync<TMessage>(TMessage item, string fieldName, object fieldValue, string partitionKey)
        where TMessage : IDbItem;
}
