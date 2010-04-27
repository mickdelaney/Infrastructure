using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Context;

namespace Md.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        void Start();

        /// <summary>
        /// Send all work done in the UnitOfWork's transaction to the database. Also starts a new transaction 
        /// ready for any additional work you do.
        /// You can call this method multiple times for during a single Unit of Work 
        /// if you want to send several updates separately.
        /// </summary>
        void Commit();

        void End();
    }

    public class UnitOfWork : IUnitOfWork
    {
        protected ISessionFactory SessionFactory;

        public UnitOfWork(ISessionFactory sessionFactory)
        {
            SessionFactory = sessionFactory;
        }

        public bool IsStarted
        {
            get
            {
                if (SessionFactory == null) return false;
                return CurrentSessionContext.HasBind(SessionFactory);
            }
        }

        public ISession Session
        {
            get { return SessionFactory.GetCurrentSession(); }
        }

        public void Start()
        {
            if (SessionFactory == null)
            {
                return;
            }

            //If UnitOfWork is called again, we dont need another session.
            if (CurrentSessionContext.HasBind(SessionFactory))
            {
                return;
            }

            var session = SessionFactory.OpenSession();
            session.SetupBeginTransaction();

            CurrentSessionContext.Bind(session);

        }

        /// <summary>
        /// Send all work done in the UnitOfWork's transaction to the database. Also starts a new transaction 
        /// ready for any additional work you do.
        /// You can call this method multiple times for during a single Unit of Work 
        /// if you want to send several updates separately.
        /// </summary>
        public void Commit()
        {
            Session.CommitTransactionIfActive();
        }

        public void End()
        {
            if (!IsStarted)
            {
                return;
            }

            ISession session = CurrentSessionContext.Unbind(SessionFactory);

            session.RollbackCloseDisposeSession();
        }
    }
}
