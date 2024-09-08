using System.Net;
using HE.FMS.Middleware.BusinessLogic.Grants.Settings;
using HE.FMS.Middleware.BusinessLogic.Mambu;
using HE.FMS.Middleware.Common.Exceptions.Communication;
using HE.FMS.Middleware.Contract.Mambu.Rotation;
using HE.FMS.Middleware.Providers.KeyVault;
using HE.FMS.Middleware.Providers.Mambu.Api.Group;
using HE.FMS.Middleware.Providers.Mambu.Api.Rotation;
using HE.FMS.Middleware.Providers.Mambu.Auth;
using HE.FMS.Middleware.Providers.Mambu.Settings;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests;

public class MambuApiKeyServiceTests
{
    private readonly IMambuRotationApiClient _mockRotationApiClient;
    private readonly IKeyVaultSecretClient _mockKeyVaultClient;
    private readonly IMambuApiKeyProvider _mockApiKeyProvider;
    private readonly MambuApiKeyService _service;

    public MambuApiKeyServiceTests()
    {
        _mockRotationApiClient = Substitute.For<IMambuRotationApiClient>();
        var mockGroupApiClient = Substitute.For<IMambuGroupApiClient>();
        _mockKeyVaultClient = Substitute.For<IKeyVaultSecretClient>();
        _mockApiKeyProvider = Substitute.For<IMambuApiKeyProvider>();
        var mockSettings = Substitute.For<IMambuApiKeySettings>();
        var mockGrantsSettings = Substitute.For<IGrantsSettings>();
        var mockLogger = Substitute.For<ILogger<MambuApiKeyService>>();

        _service = new MambuApiKeyService(
            _mockRotationApiClient,
            mockGroupApiClient,
            _mockKeyVaultClient,
            _mockApiKeyProvider,
            mockSettings,
            mockGrantsSettings,
            mockLogger);
    }

    [Fact]
    public async Task RotateApiKey_ShouldRotateAndSaveApiKey()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var rotateApiKeyResponse = new RotateApiKeyResponse("newApiKey", "newId", "newSecretKey");
        var key = new RotateApiKeyResponse("oldApiKey", "oldId", "oldSecretKey");

        _mockKeyVaultClient.Get<RotateApiKeyResponse>(Arg.Any<string>(), cancellationToken).Returns(Task.FromResult(key));
        _mockRotationApiClient.RotateApiKey(key.Id, key.ApiKey, key.SecretKey, cancellationToken).Returns(Task.FromResult(rotateApiKeyResponse));

        // Act
        await _service.RotateApiKey(cancellationToken);

        // Assert
        await _mockKeyVaultClient.Received(1).Set(Arg.Any<string>(), rotateApiKeyResponse, cancellationToken);
        _mockApiKeyProvider.Received(1).InvalidateApiKey();
    }

    [Fact]
    public async Task RotateApiKey_ShouldLogErrorAndThrow_WhenKeyVaultSetFails()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var rotateApiKeyResponse = new RotateApiKeyResponse("newApiKey", "newId", "newSecretKey");
        var key = new RotateApiKeyResponse("oldApiKey", "oldId", "oldSecretKey");

        _mockKeyVaultClient.Get<RotateApiKeyResponse>(Arg.Any<string>(), cancellationToken).Returns(Task.FromResult(key));
        _mockRotationApiClient.RotateApiKey(key.Id, key.ApiKey, key.SecretKey, cancellationToken).Returns(Task.FromResult(rotateApiKeyResponse));
        _mockKeyVaultClient
            .Set(Arg.Any<string>(), rotateApiKeyResponse, cancellationToken)
            .Returns(Task.FromException(new ExternalSystemCommunicationException("KeyVault set failed", HttpStatusCode.BadRequest)));

        // Act & Assert
        await Assert.ThrowsAsync<ExternalSystemCommunicationException>(() => _service.RotateApiKey(cancellationToken));
    }
}
