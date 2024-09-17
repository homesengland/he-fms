using System.Globalization;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Reclaims.Efin;
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
public class CLI_IW_INA
{
    [EfinFileRowIndex(1, 3)]
    public string cliwa_sub_ledger_id { get; set; }

    [EfinFileRowIndex(4, 10)]
    public string cliwa_batch_ref { get; set; }

    [EfinFileRowIndex(11, 18)]
    public string cliwa_inv_ref { get; set; }

    [EfinFileRowIndex(19, 23)]
    public string cliwa_item_sequence { get; set; }

    [EfinFileRowIndex(24, 29)]
    public string cliwa_cost_centre { get; set; }

    [EfinFileRowIndex(30, 37)]
    public string cliwa_account { get; set; }

    [EfinFileRowIndex(38, 43)]
    public string cliwa_activity { get; set; }

    [EfinFileRowIndex(44, 51)]
    public string cliwa_job { get; set; }

    [EfinFileRowIndex(52, 67)]
    public string cliwa_amount { get; set; }

    [EfinFileRowIndex(145, 147)]
    public string cliwa_uom { get; set; }

    [EfinFileRowIndex(148, 148)]
    public string cliwa_pre_pay_yn { get; set; }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
