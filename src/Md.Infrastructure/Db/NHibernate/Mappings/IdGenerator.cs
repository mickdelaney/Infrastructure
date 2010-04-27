using System;
using FluentNHibernate.Mapping;
using Md.Infrastructure.Domain;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Id;

namespace Md.Infrastructure.Data.NHibernate.Mappings
{
    public static class IdGenerator<T> where T : EntityBase<T>
    {
        public static Action<ClassMap<T>> Strategy;

        public static Action<ClassMap<T>> GetStrategy()
        {
            if (Strategy == null)
                return DefaultIdGenerationStrategy;

            return Strategy;
        }

        public static void DefaultIdGenerationStrategy(ClassMap<T> map)
        {
            map.Id(x => x.Id).GeneratedBy.GuidComb();
        }

        public static void AssignedStrategy(ClassMap<T> map)
        {
            map.Id(x => x.Id).GeneratedBy.Assigned();
        }

        public static Guid Generate(ISessionImplementor session, object obj)
        {
            return new Guid(new GuidCombGenerator().Generate(session, obj).ToString());
        }
    }
}