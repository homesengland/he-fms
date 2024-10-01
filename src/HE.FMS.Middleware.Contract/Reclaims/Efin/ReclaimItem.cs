namespace HE.FMS.Middleware.Contract.Reclaims.Efin;
public class ReclaimItem
{
    public CLI_IW_ILT CliIwIlt { get; set; }

    public CLI_IW_INA CliIwIna { get; set; }

    public CLI_IW_INL CliIwInl { get; set; }

    public CLI_IW_INV CliIwInv { get; set; }

    public CLI_IW_ITL CliIwItl { get; set; }

    public void SetBatchRef(string batchRef)
    {
        CliIwIlt.cliwt_batch_ref = batchRef;
        CliIwIna.cliwa_batch_ref = batchRef;
        CliIwInl.cliwl_batch_ref = batchRef;
        CliIwInv.cliwi_batch_ref = batchRef;
        CliIwItl.cliwx_batch_ref = batchRef;
    }
}
