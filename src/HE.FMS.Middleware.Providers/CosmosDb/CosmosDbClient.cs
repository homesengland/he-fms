using System.Linq.Expressions;
using System.Reflection;
using Azure.Identity;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace HE.FMS.Middleware.Providers.CosmosDb;

internal sealed class CosmosDbClient : ICosmosDbClient, IDisposable
{
    private readonly CosmosClient _client;

    private readonly Container _container;

    public CosmosDbClient(ICosmosDbSettings settings)
    {
        _client = !string.IsNullOrEmpty(settings.AccountEndpoint)
            ? new CosmosClient(accountEndpoint: settings.AccountEndpoint, new DefaultAzureCredential())
            : new CosmosClient(connectionString: settings.ConnectionString);

        var database = _client.GetDatabase(settings.DatabaseId);
        _container = database.GetContainer(settings.ContainerId);
    }

    public async Task UpsertItem<TMessage>(TMessage message, CancellationToken cancellationToken)
        where TMessage : ICosmosDbItem =>
        await _container.UpsertItemAsync(
            item: message,
            partitionKey: new PartitionKey(message.PartitionKey),
            cancellationToken: cancellationToken);

    public async Task<IList<TMessage>> FindAllItems<TMessage>(Expression<Func<TMessage, bool>> predicate, string partitionKey)
        where TMessage : ICosmosDbItem
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

    public async Task UpdateFieldAsync<TMessage>(TMessage item, string fieldName, object fieldValue, string partitionKey)
        where TMessage : ICosmosDbItem
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
        _client.Dispose();
    }
}
