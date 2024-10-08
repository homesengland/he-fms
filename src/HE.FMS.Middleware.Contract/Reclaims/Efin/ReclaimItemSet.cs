using HE.FMS.Middleware.Contract.Common;

// ReSharper disable InconsistentNaming
namespace HE.FMS.Middleware.Contract.Reclaims.Efin;
public class ReclaimItemSet : IItemSet
{
    public CLI_IW_BAT CLI_IW_BAT { get; set; }

    public IList<CLI_IW_ILT> CLI_IW_ILTes { get; set; } = [];

    public IList<CLI_IW_INA> CLI_IW_INAes { get; set; } = [];

    public IList<CLI_IW_INL> CLI_IW_INLes { get; set; } = [];

    public IList<CLI_IW_INV> CLI_IW_INVes { get; set; } = [];

    public IList<CLI_IW_ITL> CLI_IW_ITLes { get; set; } = [];

    public string IdempotencyKey { get; set; }

    public string BatchNumber { get; set; }
}
