namespace HE.FMS.Middleware.Providers.Mambu.Api.Rotation.Contract;

public record RotateApiKeyRequest(string ApiKey, int ExpirationTime, string Id);
