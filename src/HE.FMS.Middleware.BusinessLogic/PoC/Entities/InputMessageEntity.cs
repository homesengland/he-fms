using HE.FMS.Middleware.Providers.CosmosDb;

namespace HE.FMS.Middleware.BusinessLogic.PoC.Entities;

public record InputMessageEntity(string Id, string PartitionKey, string Name) : ICosmosDbItem;
