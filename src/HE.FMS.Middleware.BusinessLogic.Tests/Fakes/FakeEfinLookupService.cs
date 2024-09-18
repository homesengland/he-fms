using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
internal class FakeEfinLookupService : IEfinLookupCacheService
{
    public Task<Dictionary<string, string>> GetValue(string key)
    {
        var dict = new Dictionary<string, string>();

        if (key.Equals(EfinConstants.Default.ClaimDefault, StringComparison.OrdinalIgnoreCase))
        {
            dict = new Dictionary<string, string>
            {
                { "cla_sub_ledger", "PL4" },
                { "cla_uom", "EA" },
                { "cla_unit_qty", "1" },
                { "cla_volume", "1" },
                { "clb_grouping", "C" },
                { "clb_sub_ledger", "PL4" },
                { "clb_user", "AH GRANTS" },
                { "clb_vat_amount", "0" },
                { "cli_sub_ledger", "PL4" },
                { "cli_terms_code", "00" },
                { "cli_trans_type", "PI" },
                { "cli_uom", "EA" },
                { "cli_vat", "0" },
                { "cli_volume", "1" },
            };
        }

        return Task.FromResult(dict);
    }

    public void InvalidateKey(string key)
    {
        throw new NotImplementedException();
    }
}
