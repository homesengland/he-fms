namespace HE.FMS.Middleware.Common;

public static class Constants
{
    public static class CustomHeaders
    {
        public const string IdempotencyKey = "Idempotency-Key";
    }

    public static class FunctionsTriggers
    {
        public const string ServiceBusTrigger = "serviceBusTrigger";
        public const string OrchestrationTrigger = "orchestrationTrigger";
        public const string ActivityTrigger = "activityTrigger";
        public const string HttpTrigger = "httpTrigger";
        public const string TimeTrigger = "timeTrigger";
    }

    public static class FunctionsConfiguration
    {
        public const int MaxRetryCount = 5;
        public const string DelayInterval = "00:00:30";
    }

    public static class CosmosDbConfiguration
    {
        public const string PartitonKey = "fms";
        public const string ConfigPartitonKey = "fms-config";
    }

    public static class IndexConfiguration
    {
        public static class Claim
        {
            public const string InvoiceIndex = nameof(InvoiceIndex);
            public const string InvoiceIndexPrefix = "CS";
            public const int InvoiceIndexLength = 6;

            public const string BatchIndex = nameof(BatchIndex);
            public const string BatchIndexPrefix = "K";
            public const int BatchIndexLength = 6;
        }

        public static class Reclaim
        {
            public const string InvoiceIndex = nameof(InvoiceIndex);
            public const string InvoiceIndexPrefix = "J";
            public const int InvoiceIndexLength = 7;

            public const string BatchIndex = nameof(BatchIndex);
            public const string BatchIndexPrefix = "A";
            public const int BatchIndexLength = 6;
        }
    }
}
