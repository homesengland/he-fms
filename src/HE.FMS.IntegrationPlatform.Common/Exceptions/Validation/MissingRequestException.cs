namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Validation;

public class MissingRequestException : ValidationException
{
    public MissingRequestException()
        : base(ValidationErrorCodes.MissingRequest, "Request body is missing.")
    {
    }
}
