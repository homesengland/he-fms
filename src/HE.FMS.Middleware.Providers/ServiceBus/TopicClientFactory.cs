using HE.FMS.Middleware.Common.Exceptions.Internal;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace HE.FMS.Middleware.Providers.ServiceBus;
public class TopicClientFactory : ITopicClientFactory
{
    private const string ConnectionStringSetting = "ServiceBus:Connection";

    private readonly IConfiguration _configuration;

    public TopicClientFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TopicClient GetTopicClient(string topicName)
    {
        if (string.IsNullOrWhiteSpace(topicName))
        {
            throw new ArgumentNullException(nameof(topicName));
        }

        if (string.IsNullOrWhiteSpace(_configuration[topicName]))
        {
            throw new MissingConfigurationException(nameof(topicName));
        }

        return new TopicClient(_configuration[ConnectionStringSetting], _configuration[topicName]);
    }
}
