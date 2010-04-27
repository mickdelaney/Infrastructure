using System;
using Castle.Windsor;
using NHibernate.Bytecode.Lightweight;
using NHibernate.Properties;

namespace Md.Infrastructure.Data.NHibernate
{
    public class WindsorLightweightReflectionOptimizer : ReflectionOptimizer
    {
        private readonly IWindsorContainer container;

        public WindsorLightweightReflectionOptimizer(IWindsorContainer container, Type mappedType, IGetter[] getters, ISetter[] setters)
            : base(mappedType, getters, setters)
        {
            this.container = container;
        }

        public override object CreateInstance()
        {
            if (container.Kernel.HasComponent(mappedType))
            {
                return container.Resolve(mappedType);
            }
            
            return container.Kernel.HasComponent(mappedType.FullName)
                       ? container.Resolve(mappedType.FullName)
                       : base.CreateInstance();
        }

        protected override void ThrowExceptionForNoDefaultCtor(Type type) { }
    }
}
