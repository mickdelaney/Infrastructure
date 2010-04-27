using FluentNHibernate.Mapping;
using Md.Infrastructure.Domain;

namespace Md.Infrastructure.Data.NHibernate.Mappings
{
    public abstract class EntityBaseClassMap<T> : ClassMap<T> where T : EntityBase<T>
    {
        protected EntityBaseClassMap()
        {
            IdGenerator<T>.GetStrategy()(this);
        }
    }
}
