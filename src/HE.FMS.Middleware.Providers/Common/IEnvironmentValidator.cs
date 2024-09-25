namespace HE.FMS.Middleware.Providers.Common;

public interface IEnvironmentValidator
{
    void Validate(string? environment);

    string[] GetAllowedEnvironments();
}
