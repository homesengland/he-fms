using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HE.FMS.Middleware.Providers.Common;
using Microsoft.VisualBasic;

namespace HE.FMS.Middleware.Providers.Tests.Fakes;
public class FakeDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.MinValue;

    public DateTime Today => DateTime.MinValue;

    public DateTime UtcNow => DateTime.MinValue;
}
