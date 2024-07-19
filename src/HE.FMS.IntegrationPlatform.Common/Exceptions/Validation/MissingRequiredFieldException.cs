namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Validation;

public class MissingRequiredFieldException : ValidationException
{
    public MissingRequiredFieldException(string fieldName)
        : base(ValidationErrorCodes.MissingRequiredField, $"Required field {fieldName} is missing.")
    {
    }
}
