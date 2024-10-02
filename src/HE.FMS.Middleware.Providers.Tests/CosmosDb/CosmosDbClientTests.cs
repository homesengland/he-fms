using System.Linq.Expressions;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.Providers.Tests.CosmosDb;

public class CosmosDbClientTests
{
    private readonly CosmosClient _cosmosClient;
    private readonly Database _database;
    private readonly Container _container;
    private readonly CosmosDbSettings _settings;

    public CosmosDbClientTests()
    {
        _cosmosClient = Substitute.For<CosmosClient>();
        _database = Substitute.For<Database>();
        _container = Substitute.For<Container>();

        _settings = new CosmosDbSettings
        {
            DatabaseId = "TestDatabase",
            ContainerId = "TestContainer"
        };

        _cosmosClient.GetDatabase(_settings.DatabaseId).Returns(_database);
        _database.GetContainer(_settings.ContainerId).Returns(_container);
    }

    private CosmosDbClient<TMessage> CreateCosmosDbClient<TMessage>()
        where TMessage : ICosmosItem
    {
        return Substitute.ForPartsOf<CosmosDbClient<TMessage>>(_cosmosClient, _settings);
    }

    [Fact]
    public async Task UpsertItem_ShouldCallUpsertItemAsync()
    {
        // Arrange  
        var client = CreateCosmosDbClient<TestMessage>();
        var message = new TestMessage { Id = "1", PartitionKey = "pk" };
        var cancellationToken = CancellationToken.None;

        // Act  
        await client.UpsertItem(message, cancellationToken);

        // Assert  
        await _container.Received(1).UpsertItemAsync(message, new PartitionKey(message.PartitionKey), cancellationToken: cancellationToken);
    }

    [Fact]
    public async Task GetItem_ShouldCallReadItemAsync()
    {
        // Arrange  
        var client = CreateCosmosDbClient<TestMessage>();
        var id = "1";
        var partitionKey = "pk";

        // Act  
        await client.GetItem(id, partitionKey);

        // Assert  
        await _container.Received(1).ReadItemAsync<TestMessage>(id, new PartitionKey(partitionKey));
    }

    [Fact]
    public async Task UpdateFieldAsync_ShouldUpdateFieldAndCallReplaceItemAsync()
    {
        // Arrange  
        var client = CreateCosmosDbClient<TestMessage>();
        var item = new TestMessage { Id = "1", PartitionKey = "pk", Name = "OldName" };
        var newName = "NewName";
        var fieldName = "Name";
        var partitionKey = "pk";

        // Act  
        await client.UpdateFieldAsync(item, fieldName, newName, partitionKey);

        // Assert  
        Assert.Equal(newName, item.Name);
        await _container.Received(1).ReplaceItemAsync(item, item.Id, new PartitionKey(partitionKey));
    }

    // Dummy message class for testing  
    public class TestMessage : ICosmosItem
    {
        public string Id { get; set; }

        public string PartitionKey { get; set; }

        public string Name { get; set; }
    }
}
