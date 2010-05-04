using System.Reflection;
using Castle.MicroKernel;
using Castle.Windsor;
using Md.Web.Extensions;

namespace Md.Infrastructure.Mvc.Windsor
{
    public class NamespacedControllersInstaller : IWindsorInstaller
    {
        private readonly Assembly _controllersAssembly;

        public NamespacedControllersInstaller(Assembly controllersAssembly)
        {
            _controllersAssembly = controllersAssembly;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.RegisterControllers(_controllersAssembly);
        }
    }
}