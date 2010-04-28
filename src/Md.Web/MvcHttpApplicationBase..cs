using System.Web;
using Castle.Windsor;
using log4net;
using NServiceBus;

namespace Md.Web
{
    public class MvcHttpApplicationBase<T> : HttpApplication, IContainerAccessor where T : class 
    {
        protected readonly ILog Logger = LogManager.GetLogger(typeof(T));

        protected static WindsorContainer _container;

        public static IWindsorContainer Container
        {
            get { return _container; }
        }

        IWindsorContainer IContainerAccessor.Container
        {
            get { return Container; }
        }

        public static IBus Bus { get; private set; }

    }
}