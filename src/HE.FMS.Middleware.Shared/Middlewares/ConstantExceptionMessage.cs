namespace HE.FMS.Middleware.Shared.Middlewares;
public static class ConstantExceptionMessage
{
    public const string ClaimsValidationException =
        "Failure messages are to be picked up by the Azure Admins - we will not be storing any success or failure messages to a user in D365";

    public const string ReclaimsValidationException =
        "Failure messages are to be picked up by the Azure Admins - we will not be storing any success or failure messages to a user in D365";
}
