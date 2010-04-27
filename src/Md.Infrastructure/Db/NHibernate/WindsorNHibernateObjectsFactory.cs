using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using NHibernate.Bytecode;

namespace Md.Infrastructure.Data.NHibernate
{
    public class WindsorNHibernateObjectsFactory : IObjectsFactory
    {
        private readonly IWindsorContainer container;

        public WindsorNHibernateObjectsFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        public object CreateInstance(Type type)
        {
            return container.Kernel.HasComponent(type) ? container.Resolve(type) : Activator.CreateInstance(type);
        }

        public object CreateInstance(Type type, bool nonPublic)
        {

            return container.Kernel.HasComponent(type) ? container.Resolve(type) : Activator.CreateInstance(type, nonPublic);
        }

        public object CreateInstance(Type type, params object[] ctorArgs)
        {

            return Activator.CreateInstance(type, ctorArgs);
        }
    }
}
