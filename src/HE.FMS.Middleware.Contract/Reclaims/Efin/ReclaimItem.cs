namespace HE.FMS.Middleware.Contract.Reclaims.Efin;
public class ReclaimItem
{
    public CLI_IW_ILT CliIwIlt { get; set; }

    public CLI_IW_INA CliIwIna { get; set; }

    public CLI_IW_INL CliIwInl { get; set; }

    public CLI_IW_INV CliIwInv { get; set; }

    public CLI_IW_ITL CliIwItl { get; set; }

    public void SetBatchIndex(string batchIndex)
    {
        CliIwIlt.cliwt_batch_ref = batchIndex;
        CliIwIna.cliwa_batch_ref = batchIndex;
        CliIwInl.cliwl_batch_ref = batchIndex;
        CliIwInv.cliwi_batch_ref = batchIndex;
        CliIwIlt.cliwt_batch_ref = batchIndex;
    }
}
