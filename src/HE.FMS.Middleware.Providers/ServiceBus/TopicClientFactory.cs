using HE.FMS.Middleware.Common.Exceptions.Internal;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Primitives;
using Microsoft.Extensions.Configuration;

namespace HE.FMS.Middleware.Providers.ServiceBus;
public class TopicClientFactory : ITopicClientFactory
{
    private const string ConnectionStringSetting = "ServiceBus:Connection";
    private const string FullyQualifiedNamespaceSetting = "ServiceBus:Connection:fullyQualifiedNamespace";

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

        if (string.IsNullOrWhiteSpace(_configuration[FullyQualifiedNamespaceSetting])
            && !string.IsNullOrWhiteSpace(_configuration[ConnectionStringSetting]))
        {
            return new TopicClient(_configuration[ConnectionStringSetting], _configuration[topicName]);
        }
        else if (!string.IsNullOrWhiteSpace(_configuration[FullyQualifiedNamespaceSetting])
            && string.IsNullOrWhiteSpace(_configuration[ConnectionStringSetting]))
        {
            return new TopicClient(_configuration[FullyQualifiedNamespaceSetting], _configuration[topicName], new ManagedIdentityTokenProvider());
        }
        else
        {
            throw new MissingConfigurationException(ConnectionStringSetting);
        }
    }
}
