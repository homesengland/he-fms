namespace HE.FMS.Middleware.Common.Exceptions.Internal;

public class InvalidEnvironmentException : InternalSystemException
{
    public InvalidEnvironmentException(string environment)
        : base($"Environment {environment} is not allowed for this instance.")
    {
    }

    public override string ErrorCode => InternalErrorCodes.InvalidEnvironment;
}
