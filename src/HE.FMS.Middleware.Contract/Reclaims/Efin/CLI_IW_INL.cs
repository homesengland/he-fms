using System.Globalization;
using HE.FMS.Middleware.Contract.Attributes.Efin;
using HE.FMS.Middleware.Contract.Constants;

namespace HE.FMS.Middleware.Contract.Reclaims.Efin;
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
public class CLI_IW_INL
{
    [EfinFileRowIndex(1, 3)]
    public string cliwl_sub_ledger_id { get; set; }

    [EfinFileRowIndex(4, 10)]
    public string cliwl_batch_ref { get; set; }

    [EfinFileRowIndex(11, 18)]
    public string cliwl_inv_ref { get; set; }

    [EfinFileRowIndex(19, 23)]
    public string cliwl_item_sequence { get; set; }

    [EfinFileRowIndex(24, 43)]
    public string cliwl_product_id { get; set; }

    [EfinFileRowIndex(44, 59)]
    public string cliwl_goods_value { get; set; }

    [EfinFileRowIndex(108, 109)]
    public string cliwl_vat_code { get; set; }

    [EfinFileRowIndex(118, 133)]
    public string cliwl_vat_amount { get; set; }

    [EfinFileRowIndex(142, 147)]
    public string cliwl_line_ref { get; set; }
}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore S101 // Types should be named in PascalCase
