namespace HE.FMS.Middleware.Contract.Mambu.Rotation;

public record RotateApiKeyRequest(string ApiKey, int ExpirationTime, string Id);
