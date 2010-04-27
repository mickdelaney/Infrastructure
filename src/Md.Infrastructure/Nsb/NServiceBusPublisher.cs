using Md.Infrastructure.Messaging;
using NServiceBus;

namespace Md.Infrastructure.Nsb
{
    public class NServiceBusPublisher : IPublisher
    {
        private readonly IBus _bus;

        public NServiceBusPublisher(IBus bus)
        {
            _bus = bus;
        }

        public void Publish(IMessage message)
        {
            _bus.Publish(message);
        }
    }
}
