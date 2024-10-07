using Azure.Storage.Blobs;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Providers.File;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.Providers.Tests.File;

public class FileBlobWriterTests
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _blobContainerClient;
    private readonly BlobClient _blobClient;
    private readonly FileBlobWriter _fileBlobWriter;

    public FileBlobWriterTests()
    {
        _blobServiceClient = Substitute.For<BlobServiceClient>();
        _blobContainerClient = Substitute.For<BlobContainerClient>();
        _blobClient = Substitute.For<BlobClient>();

        _blobServiceClient.GetBlobContainerClient(Arg.Any<string>()).Returns(_blobContainerClient);
        _blobContainerClient.GetBlobClient(Arg.Any<string>()).Returns(_blobClient);

        _fileBlobWriter = new FileBlobWriter(_blobServiceClient);
    }

    [Fact]
    public async Task WriteAsync_ShouldUploadBlob_WhenValidDataIsProvided()
    {
        // Arrange  
        var blobContainerName = "test-container";
        var blobData = new BlobData
        {
            Name = "test-blob",
            Content = "This is a test",
        };

        // Act  
        await _fileBlobWriter.WriteAsync(blobContainerName, blobData);

        // Assert  
        await _blobContainerClient.Received(1).CreateIfNotExistsAsync();
        await _blobClient.Received(1).UploadAsync(Arg.Any<Stream>(), true);
    }

    [Fact]
    public async Task WriteAsync_ShouldThrowArgumentNullException_WhenBlobContainerNameIsNull()
    {
        // Arrange  
        var blobData = new BlobData { Name = "test-blob", Content = "This is a test" };

        // Act & Assert  
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await _fileBlobWriter.WriteAsync(null!, blobData));
    }

    [Fact]
    public async Task WriteAsync_ShouldThrowArgumentNullException_WhenBlobDataIsNull()
    {
        // Arrange  
        var blobContainerName = "test-container";

        // Act & Assert  
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await _fileBlobWriter.WriteAsync(blobContainerName, null!));
    }

    [Fact]
    public async Task WriteAsync_ShouldThrowArgumentNullException_WhenBlobDataNameIsNull()
    {
        // Arrange  
        var blobContainerName = "test-container";
        var blobData = new BlobData { Name = null!, Content = "This is a test" };

        // Act & Assert  
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await _fileBlobWriter.WriteAsync(blobContainerName, blobData));
    }

    [Fact]
    public async Task WriteAsync_ShouldThrowArgumentNullException_WhenBlobDataContentIsNull()
    {
        // Arrange  
        var blobContainerName = "test-container";
        var blobData = new BlobData { Name = "test-blob", Content = null! };

        // Act & Assert  
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await _fileBlobWriter.WriteAsync(blobContainerName, blobData));
    }
}
