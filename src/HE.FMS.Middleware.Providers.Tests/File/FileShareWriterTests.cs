using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Providers.File;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.Providers.Tests.File;

public class FileShareWriterTests
{
    private readonly ShareClient _shareClient;
    private readonly ShareFileClient _fileClient;
    private readonly FileShareWriter _fileShareWriter;

    public FileShareWriterTests()
    {
        _shareClient = Substitute.For<ShareClient>();
        var directoryClient = Substitute.For<ShareDirectoryClient>();
        _fileClient = Substitute.For<ShareFileClient>();
        _shareClient.GetRootDirectoryClient().Returns(directoryClient);
        directoryClient.GetFileClient(Arg.Any<string>()).Returns(_fileClient);
        _fileShareWriter = new FileShareWriter(_shareClient);
    }

    [Fact]
    public async Task WriteAsync_ShouldUploadFile_WhenValidDataIsProvided()
    {
        // Arrange  
        var blobContainerName = "directory1/directory2";
        var blobData = new BlobData
        {
            Name = "test-file",
            Content = "This is a test",
        };

        // Act  
        await _fileShareWriter.WriteAsync(blobContainerName, blobData);

        // Assert  
        await _shareClient.Received(1).CreateIfNotExistsAsync();
    }

    [Fact]
    public async Task WriteAsync_ShouldThrowArgumentNullException_WhenBlobContainerNameIsNull()
    {
        // Arrange  
        var blobData = new BlobData { Name = "test-file", Content = "This is a test" };

        // Act & Assert  
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _fileShareWriter.WriteAsync(null!, blobData));
    }

    [Fact]
    public async Task WriteAsync_ShouldThrowArgumentNullException_WhenBlobDataIsNull()
    {
        // Arrange  
        var blobContainerName = "directory1/directory2";

        // Act & Assert  
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _fileShareWriter.WriteAsync(blobContainerName, null!));
    }

    [Fact]
    public async Task WriteAsync_ShouldThrowArgumentNullException_WhenBlobDataNameIsNull()
    {
        // Arrange  
        var blobContainerName = "directory1/directory2";
        var blobData = new BlobData { Name = null!, Content = "This is a test" };

        // Act & Assert  
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _fileShareWriter.WriteAsync(blobContainerName, blobData));
    }

    [Fact]
    public async Task WriteAsync_ShouldThrowArgumentNullException_WhenBlobDataContentIsNull()
    {
        // Arrange  
        var blobContainerName = "directory1/directory2";
        var blobData = new BlobData { Name = "test-file", Content = null! };

        // Act & Assert  
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _fileShareWriter.WriteAsync(blobContainerName, blobData));
    }
}
