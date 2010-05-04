using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace Md.Infrastructure.Mvc
{
    public class NamespacedControllerFactory : IControllerFactory
    {
        private readonly IWindsorContainer _container;
        private readonly Type _controllerType;

        public NamespacedControllerFactory(Type controllerType)
            : this(((IContainerAccessor) HttpContext.Current.ApplicationInstance).Container, controllerType)
        {
        }

        public NamespacedControllerFactory(IWindsorContainer container, Type controllerType)
        {
            if (container == null)
                throw new ArgumentNullException("container", "Windsor Container cannot be null");

            _container = container;
            _controllerType = controllerType;
        }

        public IController CreateController(RequestContext context, string controllerName)
        {
            controllerName = controllerName + "Controller";

            var namespaces = context.RouteData.DataTokens["namespaces"] as IEnumerable<string>;

            Controller controller = null;

            if (namespaces == null) namespaces = new[] {_controllerType.Namespace};

            foreach (string ns in namespaces)
            {
                controller = (Controller) _container.Resolve(ns.ToLower() + "." + controllerName.ToLower());

                if (controller == null)
                    continue;

                controller.ActionInvoker = _container.Resolve<IActionInvoker>();
                break;
            }

            return controller;
        }

        public void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }

            _container.Release(controller);
        }
    }
}