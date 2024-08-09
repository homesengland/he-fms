namespace HE.FMS.Middleware;

public static class Constants
{
    public static class HttpHeaders
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

    public static class CosmosDBConfiguration
    {
        public const string FMS = "fms";
    }
}
