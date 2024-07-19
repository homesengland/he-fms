﻿namespace HE.FMS.IntegrationPlatform.Common.Exceptions.Validation;

public class InvalidQueryParamException : ValidationException
{
    public InvalidQueryParamException(string message)
        : base(ValidationErrorCodes.InvalidQueryParams, message)
    {
    }
}
