using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Md.Infrastructure.Data
{
    public static class SessionExtensions
    {
        public static void RollbackCloseDisposeSession(this ISession session)
        {
            if (session.Transaction != null && session.Transaction.IsActive)
            {
                session.Transaction.Rollback();
            }

            if (session.IsOpen)
            {
                session.Close();
            }

            session.Dispose();
        }

        public static void SetupBeginTransaction(this ISession session)
        {
            session.FlushMode = FlushMode.Commit;
            session.BeginTransaction();
        }

        public static void CommitTransactionIfActive(this ISession session)
        {
            ITransaction tx = session.Transaction;

            try
            {
                if (tx != null && tx.IsActive)
                {
                    tx.Commit();
                }
            }
            catch
            {
                if (tx != null && tx.IsActive)
                {
                    tx.Rollback();
                }

                throw;
            }
            finally
            {
                if (tx != null)
                {
                    tx.Dispose();
                }
            }

            session.BeginTransaction();
        }
    }
}
