using HE.FMS.Middleware.Common;
using Microsoft.Extensions.Configuration;

namespace HE.FMS.Middleware.Providers.CosmosDb;
public class CosmosDbHelper
{
    private const string PartitonKeySetting = "CosmosDb:PartitionKey";

    private readonly IConfiguration _configuration;

    public CosmosDbHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public CosmosDbItem CreateCosmosDbItem(object value, string idempotencyKey)
    {
        return new CosmosDbItem()
        {
            Id = Guid.NewGuid().ToString(),
            PartitionKey = _configuration[PartitonKeySetting] ?? Constants.CosmosDBConfiguration.PartitonKey,
            IdempotencyKey = idempotencyKey,
            CreationTime = DateTime.Now,
            Value = value,
        };
    }
}
