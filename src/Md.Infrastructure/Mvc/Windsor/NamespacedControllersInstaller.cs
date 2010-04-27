using System;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Md.Infrastructure.Mvc.Extensions;

namespace Md.Infrastructure.Mvc.Windsor
{
    public class NamespacedControllersInstaller : IWindsorInstaller
    {
        private readonly Assembly _controllersAssembly;

        public NamespacedControllersInstaller(Assembly controllersAssembly)
        {
            _controllersAssembly = controllersAssembly;
        }

        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            foreach (Type type in _controllersAssembly.GetTypes().Where(ControllerExtensions.IsController))
            {
                container.Register(Component.For(type)
                                       .ImplementedBy(type)
                                       .Named(type.FullName.ToLower())
                                       .LifeStyle.Transient);
            }
        }

        #endregion
    }
}