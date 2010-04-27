using NHibernate;
using NHibernate.Context;
using NServiceBus;

namespace Md.Infrastructure.Nsb
{
    public class NHibernateMessageModule : IMessageModule
    {
        private readonly ISessionFactory _sessionFactory;

        public NHibernateMessageModule(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void HandleBeginMessage()
        {
            CurrentSessionContext.Bind(_sessionFactory.OpenSession());
        }

        public void HandleEndMessage()
        {
            //session is closed when the transactionscope is disposed so we
            //don't have to do anything here
        }

        public void HandleError()
        {
        }
    }
}