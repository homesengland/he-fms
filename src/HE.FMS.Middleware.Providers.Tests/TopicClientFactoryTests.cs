using HE.FMS.Middleware.Common.Exceptions.Internal;
using HE.FMS.Middleware.Providers.ServiceBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.Providers.Tests;

public class TopicClientFactoryTests
{
    private readonly IConfiguration _configuration;
    private readonly TopicClientFactory _topicClientFactory;

    public TopicClientFactoryTests()
    {
        _configuration = Substitute.For<IConfiguration>();
        _topicClientFactory = new TopicClientFactory(_configuration);
    }

    [Fact]
    public void GetTopicClient_ShouldThrowArgumentNullException_WhenTopicNameIsNullOrWhiteSpace()
    {
        // Arrange
        string topicName = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _topicClientFactory.GetTopicClient(topicName));
    }

    [Fact]
    public void GetTopicClient_ShouldThrowMissingConfigurationException_WhenTopicNameIsNotConfigured()
    {
        // Arrange
        var topicName = "test-topic";
        _configuration[topicName].Returns((string)null!);

        // Act & Assert
        Assert.Throws<MissingConfigurationException>(() => _topicClientFactory.GetTopicClient(topicName));
    }

    [Fact]
    public void GetTopicClient_ShouldReturnTopicClient_WhenConfigurationIsValid()
    {
        // Arrange
        var topicName = "test-topic";
        var endpoint = "sb://test.servicebus.windows.net/";
        var connectionString = $"Endpoint={endpoint};SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=testkey";
        var topicPath = "test-topic-path";
        _configuration[Arg.Any<string>()].Returns(connectionString);
        _configuration[topicName].Returns(topicPath);

        // Act
        var result = _topicClientFactory.GetTopicClient(topicName);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<TopicClient>(result);
        Assert.Equal(endpoint, result.ServiceBusConnection.Endpoint.ToString());
        Assert.Equal(topicPath, result.Path);
    }
}
