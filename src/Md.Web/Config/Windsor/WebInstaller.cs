﻿using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Md.Infrastructure.Mvc.Security;

namespace Md.Web.Config.Windsor
{
    public class WebInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register
            (
                Component.For<IFormsAuthenticationService>().ImplementedBy<FormsAuthenticationService>().LifeStyle.PerWebRequest
            );            

        }
    }
}