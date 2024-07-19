namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Validation;

public sealed class MissingQueryParamsException : ValidationException
{
    public MissingQueryParamsException(params string[] queryParams)
        : base(ValidationErrorCodes.MissingRequiredField, $"Required query params {string.Join(' ', queryParams)} are missing.")
    {
    }
}
