namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Rotation.Contract;

public record RotateApiKeyRequest(string ApiKey, int ExpirationTime, string Id);
