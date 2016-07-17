using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TaskManager.Core.Owin;
using TaskManagerDemo.Core.Platform.IoC;
using TaskManagerDemo.Core.Platform.IoC.Extensions;

namespace TaskManager.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest()
        {
            Container.Current.Configure((impl) =>
            {
                var container = impl as ServiceContainer;
                if (container == null)
                {
                    throw new ConcreateContainerRequiredException();
                }
                //container.BeginScope();
            });
        }
        protected void Application_EndRequest()
        {
            Container.Current.Configure((impl) =>
            {
                var container = impl as ServiceContainer;
                if (container == null)
                {
                    throw new ConcreateContainerRequiredException();
                }
                if (container.ScopeManagerProvider.GetScopeManager().CurrentScope != null)
                {
                    //container.EndCurrentScope();
                }
            });
        }

    }
}
