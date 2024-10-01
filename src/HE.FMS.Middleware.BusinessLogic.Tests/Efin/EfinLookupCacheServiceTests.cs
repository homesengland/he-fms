using HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Providers.Common.Settings;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Efin;

public class EfinLookupCacheServiceTests
{
    [Fact]
    public async Task RetrieveValue_ShouldReturnDictionary_WhenItemExists()
    {
        // Arrange  
        var key = "testKey";
        var partitionKey = Common.Constants.CosmosDbConfiguration.PartitonKey;

        var mockLookupClient = Substitute.For<IEfinLookupCosmosClient>();
        var mockMemoryCache = Substitute.For<IMemoryCache>();
        var settings = new MemoryCacheSettings();

        var json = JObject.FromObject(new Dictionary<string, string>
        {
            { "Key1", "Value1" },
            { "Key2", "Value2" },
        });

        var cosmosDbItem = new EfinLookupItem
        {
            Value = json,
        };

        mockLookupClient.GetItem(key, partitionKey).Returns(cosmosDbItem);

        var cacheService = new EfinLookupCacheServiceTestable(mockLookupClient, mockMemoryCache, settings);

        // Act  
        var result = await cacheService.RetrieveValue(key);

        // Assert  
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Value1", result["Key1"]);
        Assert.Equal("Value2", result["Key2"]);
    }

    [Fact]
    public async Task RetrieveValue_ShouldThrowMissingCosmosDbItemException_WhenItemIsNull()
    {
        // Arrange  
        var key = "testKey";
        var partitionKey = Common.Constants.CosmosDbConfiguration.PartitonKey;

        var mockLookupClient = Substitute.For<IEfinLookupCosmosClient>();
        var mockMemoryCache = Substitute.For<IMemoryCache>();
        var settings = new MemoryCacheSettings();

        mockLookupClient.GetItem(key, partitionKey).Returns(Task.FromResult<EfinLookupItem>(null!));

        var cacheService = new EfinLookupCacheServiceTestable(mockLookupClient, mockMemoryCache, settings);

        // Act & Assert  
        await Assert.ThrowsAsync<MissingCosmosDbItemException>(() => cacheService.RetrieveValue(key));
    }

    [Fact]
    public async Task RetrieveValue_ShouldThrowInvalidCastException_WhenItemValueIsInvalid()
    {
        // Arrange  
        var key = "testKey";
        var partitionKey = Common.Constants.CosmosDbConfiguration.PartitonKey;

        var mockLookupClient = Substitute.For<IEfinLookupCosmosClient>();
        var mockMemoryCache = Substitute.For<IMemoryCache>();
        var settings = new MemoryCacheSettings();

        var invalidJson = new JObject { { "Key1", new JObject { { "SubKey", "SubValue" } } } };
        var cosmosDbItem = new EfinLookupItem { Value = invalidJson };

        mockLookupClient.GetItem(key, partitionKey).Returns(cosmosDbItem);

        var cacheService = new EfinLookupCacheServiceTestable(mockLookupClient, mockMemoryCache, settings);

        // Act & Assert  
        await Assert.ThrowsAsync<InvalidCastException>(() => cacheService.RetrieveValue(key));
    }
}
