using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Castle.Windsor;
using Md.Infrastructure.Windsor;

namespace Md.Infrastructure.Mvc.Windsor
{
    public class WindsorActionInvoker : ControllerActionInvoker
    {
        private readonly IWindsorContainer container;

        public WindsorActionInvoker(IWindsorContainer container)
        {
            this.container = container;
        }

        protected override AuthorizationContext InvokeAuthorizationFilters(ControllerContext controllerContext, IList<IAuthorizationFilter> filters, ActionDescriptor actionDescriptor)
        {
            filters.ToList().ForEach(container.Kernel.InjectProperties);
            return base.InvokeAuthorizationFilters(controllerContext, filters, actionDescriptor);
        }

        protected override ActionExecutedContext InvokeActionMethodWithFilters(ControllerContext controllerContext, IList<IActionFilter> filters,
                ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            filters.ToList().ForEach(container.Kernel.InjectProperties);
            return base.InvokeActionMethodWithFilters(controllerContext, filters, actionDescriptor, parameters);
        }
    }
}
