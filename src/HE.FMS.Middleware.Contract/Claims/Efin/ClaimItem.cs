namespace HE.FMS.Middleware.Contract.Claims.Efin;
public class ClaimItem
{
    public CLI_Invoice CliInvoice { get; set; }

    public CLA_InvoiceAnalysis ClaInvoiceAnalysis { get; set; }

    public void SetBatchRef(string batchRef)
    {
        CliInvoice.cli_batch_ref = batchRef;
        ClaInvoiceAnalysis.cla_batch_ref = batchRef;
    }
}
