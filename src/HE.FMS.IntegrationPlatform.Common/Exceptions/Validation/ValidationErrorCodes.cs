namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Validation;

internal static class ValidationErrorCodes
{
    public static string InvalidRequest => nameof(InvalidRequest);

    public static string MissingRequest => nameof(MissingRequest);

    public static string MissingRequiredField => nameof(MissingRequiredField);

    public static string FailedSerialization => nameof(FailedSerialization);

    public static string InvalidQueryParams => nameof(InvalidQueryParams);
}
