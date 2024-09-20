using System.Linq.Expressions;
using System.Reflection;
using Azure.Identity;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace HE.FMS.Middleware.Providers.CosmosDb;

public abstract class CosmosDbClient<TMessage> : ICosmosDbClient<TMessage>, IDisposable
    where TMessage : ICosmosItem
{
    private readonly CosmosClient _client;

    private readonly Container _container;

    protected CosmosDbClient(CosmosClient client, CosmosDbSettings settings)
    {
        _client = client;

        var database = _client.GetDatabase(settings.DatabaseId);

        _container = database.GetContainer(settings.ContainerId);
    }

    public async Task UpsertItem(TMessage message, CancellationToken cancellationToken)
    {
        await _container.UpsertItemAsync(
            item: message,
            partitionKey: new PartitionKey(message.PartitionKey),
            cancellationToken: cancellationToken);
    }

    public async Task<TMessage> GetItem(string id, string partitionKey)
    {
        return await _container.ReadItemAsync<TMessage>(id, new PartitionKey(partitionKey));
    }

    public async Task<IList<TMessage>> FindAllItems(Expression<Func<TMessage, bool>> predicate, string partitionKey)
    {
        var queryable = _container
            .GetItemLinqQueryable<TMessage>(requestOptions: new QueryRequestOptions { PartitionKey = new PartitionKey(partitionKey) })
            .Where(predicate).ToFeedIterator();
        var results = new List<TMessage>();

        while (queryable.HasMoreResults)
        {
            results.AddRange(await queryable.ReadNextAsync());
        }

        return results;
    }

    public async Task UpdateFieldAsync(TMessage item, string fieldName, object fieldValue, string partitionKey)
    {
        var property = typeof(TMessage).GetProperty(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
        if (property != null && property.CanWrite)
        {
            property.SetValue(item, fieldValue);
        }

        await _container.ReplaceItemAsync(item, item.Id, new PartitionKey(partitionKey));
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _client.Dispose();
        }
    }
}
