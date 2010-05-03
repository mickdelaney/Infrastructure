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
            foreach (var actionFilter in filters)
                container.Kernel.InjectProperties(actionFilter);

            return base.InvokeAuthorizationFilters(controllerContext, filters, actionDescriptor);
        }
    }
}
