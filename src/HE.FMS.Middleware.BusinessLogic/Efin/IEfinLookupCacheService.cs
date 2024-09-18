using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HE.FMS.Middleware.Providers.Common;

namespace HE.FMS.Middleware.BusinessLogic.Efin;
public interface IEfinLookupCacheService : IMemoryCacheProvider<Dictionary<string, string>>
{
}
