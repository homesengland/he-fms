using HE.FMS.Middleware.Providers.Common;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public interface IEfinLookupCacheService : IMemoryCacheProvider<Dictionary<string, string>>
{
}
