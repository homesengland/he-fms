using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.FMS.Middleware.Contract.Common.CosmosDb;
public class CosmosDataItem : CosmosBaseItem
{
    public DateTimeOffset? CreationTime { get; set; }

    public string IdempotencyKey { get; set; }

    public CosmosDbItemType Type { get; set; }
}
