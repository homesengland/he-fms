using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Providers.Common.Settings;

namespace HE.FMS.Middleware.Providers.Common;
public class EnvironmentValidator : IEnvironmentValidator
{
    private readonly string[] _allowedEnvironments;

    public EnvironmentValidator(AllowedEnvironmentSettings settings)
    {
        _allowedEnvironments = settings.Environments;
    }

    public void Validate(string? environment)
    {
        if (string.IsNullOrWhiteSpace(environment))
        {
            throw new InvalidEnvironmentException("null");
        }

        if (!_allowedEnvironments.Contains(environment, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidEnvironmentException(environment);
        }
    }

    public string[] GetAllowedEnvironments()
    {
        return _allowedEnvironments;
    }
}
