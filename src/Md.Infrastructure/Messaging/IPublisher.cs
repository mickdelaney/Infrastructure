using System;
using NServiceBus;

namespace Md.Infrastructure.Messaging
{
    public interface IPublisher
    {
        void Publish(IMessage message);
    }

    public class StubPublisher : IPublisher
    {
        public void Publish(IMessage message)
        {
           
        }
    }
}