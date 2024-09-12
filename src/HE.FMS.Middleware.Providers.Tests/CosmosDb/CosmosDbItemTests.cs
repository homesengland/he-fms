using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Trace;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.Providers.Tests.CosmosDb;

public class CosmosDbItemTests
{
    private readonly IConfiguration _configuration;

    public CosmosDbItemTests()
    {
        _configuration = Substitute.For<IConfiguration>();
    }

    [Fact]
    public void CreateCosmosDbItem_ShouldReturnCosmosDbItem_WithCorrectValues()
    {
        // Arrange
        var value = new { Name = "Test" };
        var idempotencyKey = "test-key";
        var partitionKey = "fms";
        _configuration["CosmosDb:PartitionKey"].Returns(partitionKey);

        // Act
        var result = TraceItem.CreateTraceItem(value, idempotencyKey, CosmosDbItemType.Log);

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
        var result = TraceItem.CreateTraceItem(value, idempotencyKey, CosmosDbItemType.Log);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(Constants.CosmosDbConfiguration.PartitonKey, result.PartitionKey);
        Assert.Equal(idempotencyKey, result.IdempotencyKey);
        Assert.Equal(value, result.Value);
        Assert.NotEqual(default, result.CreationTime);
        Assert.False(string.IsNullOrEmpty(result.Id));
    }
}