using FluentAssertions;
using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Providers.CosmosDb.Base;
using HE.FMS.Middleware.Providers.CosmosDb.Trace;
using Xunit;

namespace HE.FMS.Middleware.Providers.Tests.CosmosDb.Trace;

public class TraceItemTests
{
    [Fact]
    public void CreateTraceItem_ShouldInitializePropertiesCorrectly()
    {
        // Arrange  
        var value = new { Property1 = "Value1", Property2 = "Value2" };
        var idempotencyKey = "TestIdempotencyKey";
        var type = CosmosDbItemType.Claim;

        // Act  
        var item = TraceItem.CreateTraceItem(value, idempotencyKey, type);

        // Assert  
        item.Should().NotBeNull();
        item.Id.Should().NotBeNullOrEmpty();
        item.PartitionKey.Should().Be(Constants.CosmosDbConfiguration.PartitonKey);
        item.IdempotencyKey.Should().Be(idempotencyKey);
        item.CreationTime.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        item.Value.Should().Be(value);
        item.Type.Should().Be(type);
    }
}
