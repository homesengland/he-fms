using FluentAssertions;
using HE.FMS.Middleware.Contract.Common.CosmosDb;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using Xunit;

namespace HE.FMS.Middleware.Providers.Tests.CosmosDb.Efin;

public class EfinConfigItemTests
{
    [Fact]
    public void Create_ShouldInitializePropertiesCorrectly()
    {
        // Arrange  
        var partitionKey = "TestPartitionKey";
        var type = CosmosDbItemType.Claim;
        var indexName = "TestIndex";
        var indexLength = 5;
        var prefix = "PREFIX";

        // Act  
        var item = EfinIndexItem.Create(partitionKey, type, indexName, indexLength, prefix);

        // Assert  
        item.Should().NotBeNull();
        item.Id.Should().NotBeNullOrEmpty();
        item.PartitionKey.Should().Be(partitionKey);
        item.Type.Should().Be(type);
        item.IndexName.Should().Be(indexName);
        item.Index.Should().Be(0);
        item.IndexLength.Should().Be(indexLength);
        item.Prefix.Should().Be(prefix);
    }

    [Fact]
    public void GetNextIndex_ShouldIncrementIndexAndReturnFormattedString()
    {
        // Arrange  
        var item = new EfinIndexItem
        {
            Index = 0,
            IndexLength = 5,
            Prefix = "PREFIX",
        };

        // Act  
        var result = item.GetNextId();

        // Assert  
        item.Index.Should().Be(1);
        result.Should().Be("PREFIX00001");
    }

    [Fact]
    public void GetNextIndex_ShouldHandleMultipleIncrements()
    {
        // Arrange  
        var item = new EfinIndexItem
        {
            Index = 0,
            IndexLength = 3,
            Prefix = "IDX",
        };

        // Act  
        var firstResult = item.GetNextId();
        var secondResult = item.GetNextId();
        var thirdResult = item.GetNextId();

        // Assert  
        item.Index.Should().Be(3);
        firstResult.Should().Be("IDX001");
        secondResult.Should().Be("IDX002");
        thirdResult.Should().Be("IDX003");
    }
}
