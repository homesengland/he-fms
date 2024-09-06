using Azure.Identity;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;
using Microsoft.Azure.Cosmos;

namespace HE.FMS.Middleware.Providers.CosmosDb;

internal sealed class CosmosDbClient : ICosmosDbClient, IDisposable
{
    private readonly ICosmosDbSettings _settings;

    private readonly CosmosClient _client;

    public CosmosDbClient(ICosmosDbSettings settings)
    {
        _settings = settings;
        _client = !string.IsNullOrEmpty(_settings.AccountEndpoint)
            ? new CosmosClient(accountEndpoint: _settings.AccountEndpoint, new DefaultAzureCredential())
            : new CosmosClient(connectionString: _settings.ConnectionString);
    }

    public async Task UpsertItem<TMessage>(TMessage message, CancellationToken cancellationToken)
        where TMessage : ICosmosDbItem
    {
        var database = _client.GetDatabase(_settings.DatabaseId);
        var container = database.GetContainer(_settings.ContainerId);

        await container.UpsertItemAsync(
            item: message,
            partitionKey: new PartitionKey(message.PartitionKey),
            cancellationToken: cancellationToken);
    }

    public async Task<List<TMessage>> GetItems<TMessage>(QueryDefinition definition, string partitionKey)
        where TMessage : ICosmosDbItem
    {
        var database = _client.GetDatabase(_settings.DatabaseId);
        var container = database.GetContainer(_settings.ContainerId);

        var feedIterator = container.GetItemQueryIterator<TMessage>(definition, null, new QueryRequestOptions { PartitionKey = new PartitionKey(partitionKey) });

        var output = new List<TMessage>();

        while (feedIterator.HasMoreResults)
        {
            var items = await feedIterator.ReadNextAsync();
            if (items.Count > 0)
            {
                output.AddRange(items);
            }
        }

        return output;
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
