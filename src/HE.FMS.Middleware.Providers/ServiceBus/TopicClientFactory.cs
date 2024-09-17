using HE.FMS.Middleware.Common;
using HE.FMS.Middleware.Common.Exceptions.Internal;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Primitives;
using Microsoft.Extensions.Configuration;

namespace HE.FMS.Middleware.Providers.ServiceBus;
public class TopicClientFactory : ITopicClientFactory
{
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

        if (string.IsNullOrWhiteSpace(_configuration[Constants.Settings.ServiceBus.FullyQualifiedNamespace])
            && !string.IsNullOrWhiteSpace(_configuration[Constants.Settings.ServiceBus.ConnectionString]))
        {
            return new TopicClient(_configuration[Constants.Settings.ServiceBus.ConnectionString], _configuration[topicName]);
        }
        else if (!string.IsNullOrWhiteSpace(_configuration[Constants.Settings.ServiceBus.FullyQualifiedNamespace])
            && string.IsNullOrWhiteSpace(_configuration[Constants.Settings.ServiceBus.ConnectionString]))
        {
            return new TopicClient(_configuration[Constants.Settings.ServiceBus.FullyQualifiedNamespace], _configuration[topicName], new ManagedIdentityTokenProvider());
        }
        else
        {
            throw new MissingConfigurationException(Constants.Settings.ServiceBus.ConnectionString);
        }
    }
}
