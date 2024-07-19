namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Validation;

public class InvalidRequestException : ValidationException
{
    public InvalidRequestException(params string[] validationMessages)
        : base(ValidationErrorCodes.InvalidRequest, "Request body is invalid. " + string.Join(' ', validationMessages))
    {
    }
}
