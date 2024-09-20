using System.Linq.Expressions;
using HE.FMS.Middleware.Contract.Common.CosmosDb;

namespace HE.FMS.Middleware.Providers.CosmosDb;

public interface ICosmosDbClient<TMessage>
    where TMessage : ICosmosItem
{
    Task<TMessage> GetItem(string id, string partitionKey);

    Task UpsertItem(TMessage message, CancellationToken cancellationToken);

    Task<IList<TMessage>> FindAllItems(Expression<Func<TMessage, bool>> predicate, string partitionKey);

    Task UpdateFieldAsync(TMessage item, string fieldName, object fieldValue, string partitionKey);
}
