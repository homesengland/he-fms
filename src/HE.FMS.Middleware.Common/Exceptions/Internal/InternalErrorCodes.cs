using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.FMS.Middleware.Common.Exceptions.Internal;
public static class InternalErrorCodes
{
    public static string MissingConfiguration => nameof(MissingConfiguration);
    public static string MisingCosmosDbItem => nameof(MisingCosmosDbItem);
}
