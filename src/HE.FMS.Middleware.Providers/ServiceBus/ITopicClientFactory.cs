using Microsoft.Azure.ServiceBus;

namespace HE.FMS.Middleware.Providers.ServiceBus;
public interface ITopicClientFactory
{
    TopicClient GetTopicClient(string topicName);
}
