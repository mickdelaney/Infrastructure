using System.Web.Mvc;
using Md.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;
using NHibernate;

namespace Md.Web.Mvc.Transactions
{
    public class ExecuteInTransaction : ActionFilterAttribute
    {
        private readonly IUnitOfWork _hostUnitofWork;

        public ExecuteInTransaction()
        {
            _hostUnitofWork = new UnitOfWork(ServiceLocator.Current.GetInstance<ISessionFactory>());
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _hostUnitofWork.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                _hostUnitofWork.Commit();
            }
            else
            {
                _hostUnitofWork.End();
            }
        }
    }
}
