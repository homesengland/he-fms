using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace HE.FMS.Middleware.Providers.Common;
public class FakeDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => new(2000, 1, 1, 1, 1, 1, DateTimeKind.Utc);

    public DateTime Today => new(2000, 1, 1, 1, 1, 1, DateTimeKind.Utc);

    public DateTime UtcNow => new(2000, 1, 1, 1, 1, 1, DateTimeKind.Utc);
}
