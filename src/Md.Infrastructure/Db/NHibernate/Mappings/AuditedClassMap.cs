using FluentNHibernate.Mapping;
using Md.Infrastructure.Domain;
using Md.Infrastructure.Security;

namespace Md.Infrastructure.Data.NHibernate.Mappings
{
    public abstract class AuditedClassMap<T> : EntityBaseClassMap<T> where T : EntityBase<T>, IHaveAuditInformation
    {
        protected AuditedClassMap()
        {
            Map(x => x.CreatedAt).Not.Update();
            Map(x => x.CreatedBy).Not.Update();
            Map(x => x.UpdatedAt);
            Map(x => x.UpdatedBy);
        }
    }
}
