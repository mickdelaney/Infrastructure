using Castle.Core.Logging;
using NServiceBus;

namespace Md.Infrastructure.Messaging
{
    public abstract class BaseMessageHandler<T> : IHandleMessages<T> where T : IMessage
    {
        private ILogger logger;

        public ILogger Logger
        {
            get { return logger ?? (logger = NullLogger.Instance); }
            set { logger = value; }
        }        

        public abstract void Handle(T message);
    }
}
