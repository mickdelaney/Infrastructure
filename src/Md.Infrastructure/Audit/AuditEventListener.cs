using System;
using System.Security.Principal;
using Md.Infrastructure.Security;
using NHibernate.Event;
using NHibernate.Persister.Entity;

namespace Md.Infrastructure.Audit
{
    public class AuditEventListener : IPreUpdateEventListener, IPreInsertEventListener
    {
        private readonly ILocalContext _context;

        public AuditEventListener(ILocalContext context)
        {
            _context = context;
        }

        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            var audit = @event.Entity as IHaveAuditInformation;
            if (audit == null)
                return false;

            var time = DateTime.Now;
            var user = GetUser();

            Set(@event.Persister, @event.State, "UpdatedAt", time);
            Set(@event.Persister, @event.State, "UpdatedBy", user);

            audit.UpdatedAt = time;
            audit.UpdatedBy = user;

            return false;
        }

        public bool OnPreInsert(PreInsertEvent @event)
        {
            var audit = @event.Entity as IHaveAuditInformation;
            if (audit == null)
                return false;

            var time = DateTime.Now;
            var user = GetUser();

            Set(@event.Persister, @event.State, "CreatedAt", time);
            Set(@event.Persister, @event.State, "UpdatedAt", time);
            Set(@event.Persister, @event.State, "CreatedBy", user);
            Set(@event.Persister, @event.State, "UpdatedBy", user);

            audit.CreatedAt = time;
            audit.CreatedBy = user;
            audit.UpdatedAt = time;
            audit.UpdatedBy = user;

            return false;
        }

        private static void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }


        private string GetUser()
        {
            return _context.Retrieve<IPrincipal>().Identity.Name;
        }

    }
}