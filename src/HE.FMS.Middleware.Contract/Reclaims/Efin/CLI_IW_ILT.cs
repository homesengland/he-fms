using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Reclaims.Efin;
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
public class CLI_IW_ILT
{
    [EfinFileRowIndex(1, 3)]
    public string cliwt_sub_ledger_id { get; set; }

    [EfinFileRowIndex(4, 10)]
    public string cliwt_batch_ref { get; set; }

    [EfinFileRowIndex(11, 18)]
    public string cliwt_inv_ref { get; set; }

    [EfinFileRowIndex(19, 23)]
    public string cliwt_item_sequence { get; set; }

    [EfinFileRowIndex(24, 27)]
    public string cliwt_print_sequence { get; set; }

    [EfinFileRowIndex(28, 102)]
    public string cliwt_text { get; set; }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
