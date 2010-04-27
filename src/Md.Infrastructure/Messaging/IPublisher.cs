using NServiceBus;

namespace Md.Infrastructure.Messaging
{
    public interface IPublisher
    {
        void Publish(IMessage message);
    }
}