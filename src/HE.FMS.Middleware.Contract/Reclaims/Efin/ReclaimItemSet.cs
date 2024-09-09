using HE.FMS.Middleware.Contract.Common;

namespace HE.FMS.Middleware.Contract.Reclaims.Efin;
public class ReclaimItemSet : IItemSet
{
    public CLI_IW_BAT CLI_IW_BAT { get; set; }

    public IList<CLI_IW_ILT> CLI_IW_ILTes { get; set; } = new List<CLI_IW_ILT>();

    public IList<CLI_IW_INA> CLI_IW_INAes { get; set; } = new List<CLI_IW_INA>();

    public IList<CLI_IW_INL> CLI_IW_INLes { get; set; } = new List<CLI_IW_INL>();

    public IList<CLI_IW_INV> CLI_IW_INVes { get; set; } = new List<CLI_IW_INV>();

    public IList<CLI_IW_ITL> CLI_IW_ITLes { get; set; } = new List<CLI_IW_ITL>();

    public string IdempotencyKey { get; set; }
}
