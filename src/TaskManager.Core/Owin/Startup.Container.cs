using LightInject;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManagerDemo.Core.Mvc.WebApi.Filters;
using TaskManagerDemo.Core.Platform.Context;
using TaskManagerDemo.Core.Platform.IoC;
using TaskManagerDemo.Core.Platform.IoC.Extensions;
using TaskManagerDemo.Core.Platform.Mapping;
using TaskManagerDemo.Core.Platform.Mapping.AutoMapper;
using TaskManagerDemo.Core.Storage.Orm.UnitOfWork;
using TaskManagerDemo.Core.Storage.Orm.UnitOfWork.NHibernate;
using TaskManagerDemo.Core.Storage.Repositories.Common;
using LightInject.WebApi;
using TaskManagerDemo.Core.Platform.Identity;
using TaskManagerDemo.Core.Platform.Sockets.SignalR;
using TaskManagerDemo.Core.Storage.Entities.Sockets;
using Microsoft.Owin.Security.OAuth;
using TaskManager.Web.Providers;
using Microsoft.AspNet.Identity;
using TaskManager.Web.Models;
using Microsoft.AspNet.SignalR;
using System.Web;
using LightInject.Web;
using TaskManagerDemo.Core.Platform.Sockets;

namespace TaskManager.Core.Owin
{
    public partial class Startup
    {
        public void ConfigureContainer(IAppBuilder app)
        {
            Container.Current.Configure((impl) =>
            {
                var container = impl as ServiceContainer;
                if(container == null)
                {
                    throw new ConcreateContainerRequiredException();
                }
                container.RegisterApiControllers();
                container.RegisterControllers();

                container.Register<IContextProvider, ContextProviderWrapper>();
                container.Register<IUnitOfWorkFactory, UnitOfWorkFactory>();
                container.Register<UnitOfWorkAttribute>();
                container.Register<IUnitOfWork>((factory) => factory.GetInstance<IUnitOfWorkFactory>().GetContextUnit());
                container.RegisterAssembly(Assembly.GetExecutingAssembly(), (service, serviceImpl) =>
                {
                    if(service.Name.Contains("IRepository"))
                    {
                        Console.Write(1);
                    }
                    return service.IsInterface && service.IsGenericType &&  service.GetGenericTypeDefinition() == typeof(IRepository<>);
                });

                container.Register<IModelMapper, AutoMapperModelMapper>(new LightInject.PerContainerLifetime());
                container.Register<IIdentityProvider, ThreadPrincipalProvider>();
                container.Register<ISocketServer, SignalRSocketServer>();
                container.Register<CommonHub>();
                container.Register<UserManager<ApplicationUser>>((factory) => ApplicationUserManager.Create(), new PerRequestLifeTime());
                container.Register<IOAuthAuthorizationServerProvider>((factory) => new ApplicationOAuthProvider(Startup.PublicClientId, factory.GetInstance<IUnitOfWorkFactory>(), factory.GetInstance<Lazy<UserManager<ApplicationUser>>>()));
                container.EnableMvc();
                container.EnableWebApi(GlobalConfiguration.Configuration);
            });
        }
    }
}
