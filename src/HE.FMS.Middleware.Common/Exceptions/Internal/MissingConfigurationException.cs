namespace HE.FMS.Middleware.Common.Exceptions.Internal;
public class MissingConfigurationException : InternalSystemException
{
    public MissingConfigurationException(string configName)
        : base($"Required configuration {configName} is missing.")
    {
    }

    public override string ErrorCode => InternalErrorCodes.MissingConfiguration;
}
