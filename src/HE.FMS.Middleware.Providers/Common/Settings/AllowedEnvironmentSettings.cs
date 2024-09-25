using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.FMS.Middleware.Providers.Common.Settings;

public class AllowedEnvironmentSettings(string environments)
{
    public string[] Environments { get; set; } = environments.Split(',');
}
