using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Providers.CosmosDb;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.Providers.Tests;

public class CosmosDbHelperTests
{
    private readonly IConfiguration _configuration;
    private readonly CosmosDbHelper _cosmosDbHelper;

    public CosmosDbHelperTests()
    {
        _configuration = Substitute.For<IConfiguration>();
        _cosmosDbHelper = new CosmosDbHelper(_configuration);
    }

    [Fact]
    public void CreateCosmosDbItem_ShouldReturnCosmosDbItem_WithCorrectValues()
    {
        // Arrange
        var value = new { Name = "Test" };
        var idempotencyKey = "test-key";
        var partitionKey = "test-partition-key";
        _configuration["CosmosDb:PartitionKey"].Returns(partitionKey);

        // Act
        var result = _cosmosDbHelper.CreateCosmosDbItem(value, idempotencyKey);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(partitionKey, result.PartitionKey);
        Assert.Equal(idempotencyKey, result.IdempotencyKey);
        Assert.Equal(value, result.Value);
        Assert.NotEqual(default, result.CreationTime);
        Assert.False(string.IsNullOrEmpty(result.Id));
    }

    [Fact]
    public void CreateCosmosDbItem_ShouldUseDefaultPartitionKey_WhenConfigurationIsNull()
    {
        // Arrange
        var value = new { Name = "Test" };
        var idempotencyKey = "test-key";
        _configuration["CosmosDb:PartitionKey"].Returns((string)null!);

        // Act
        var result = _cosmosDbHelper.CreateCosmosDbItem(value, idempotencyKey);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(Constants.CosmosDBConfiguration.PartitonKey, result.PartitionKey);
        Assert.Equal(idempotencyKey, result.IdempotencyKey);
        Assert.Equal(value, result.Value);
        Assert.NotEqual(default, result.CreationTime);
        Assert.False(string.IsNullOrEmpty(result.Id));
    }
}