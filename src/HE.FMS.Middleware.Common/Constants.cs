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
}
