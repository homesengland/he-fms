using HE.FMS.Middleware.Providers.CosmosDb.Settings;

namespace HE.FMS.Middleware.Providers.CosmosDb.Efin;

public class EfinClient : CosmosDbClient, IEfinClient
{
    public EfinClient(ICosmosDbSettings settings) : base(settings)
    { }
}
