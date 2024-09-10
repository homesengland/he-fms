using System.Linq.Expressions;
using HE.FMS.Middleware.Providers.CosmosDb.Base;

namespace HE.FMS.Middleware.Providers.CosmosDb;

public interface ICosmosDbClient<TMessage>
    where TMessage : ICosmosItem
{
    Task UpsertItem(TMessage message, CancellationToken cancellationToken);

    Task<IList<TMessage>> FindAllItems(Expression<Func<TMessage, bool>> predicate, string partitionKey);

    Task UpdateFieldAsync(TMessage item, string fieldName, object fieldValue, string partitionKey);
}
