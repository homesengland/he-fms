using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb;
using HE.FMS.Middleware.Providers.CosmosDb.Settings;
using Microsoft.Azure.Cosmos;

namespace HE.FMS.Middleware.BusinessLogic.Efin.CosmosDb;
public class EfinLookupCosmosClient : CosmosDbClient<EfinLookupItem>, IEfinLookupCosmosClient
{
    public EfinLookupCosmosClient(CosmosClient client, CosmosDbSettings settings)
        : base(client, settings)
    {
    }
}
