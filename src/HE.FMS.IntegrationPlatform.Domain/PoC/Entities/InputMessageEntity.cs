using HE.FMS.IntegrationPlatform.Providers.CosmosDb;

namespace HE.FMS.IntegrationPlatform.Domain.PoC.Entities;

public record InputMessageEntity(string Id, string PartitionKey, string Name) : ICosmosDbItem;
