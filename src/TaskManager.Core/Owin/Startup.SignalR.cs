using LightInject;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDemo.Core.Platform.IoC;
using TaskManagerDemo.Core.Platform.IoC.Extensions;
using TaskManagerDemo.Core.Platform.Sockets.SignalR;

namespace TaskManager.Core.Owin
{
    public partial class Startup
    {
        /// <summary>
        /// Конфигурирование SignalR
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureSignalR(IAppBuilder app)
        {
            Container.Current.Configure((impl) =>
            {
                var container = impl as ServiceContainer;
                if (container == null)
                {
                    throw new ConcreateContainerRequiredException();
                }
                
                var resolver = new ContainerSignalRDependencyResolver(container);
                container.Register<IHubConnectionContext<dynamic>>((factory) => resolver.Resolve<IConnectionManager>().GetHubContext<CommonHub>().Clients);
                
                var config = new HubConfiguration();
                config.Resolver = resolver;
                app.MapSignalR(config);
                 
            });
        }

        internal class ContainerSignalRDependencyResolver : DefaultDependencyResolver
        {
            private readonly ServiceContainer _kernel;
            public ContainerSignalRDependencyResolver(ServiceContainer kernel)
            {
                _kernel = kernel;
            }

            public override object GetService(Type serviceType)
            {
                return _kernel.TryGetInstance(serviceType) ?? base.GetService(serviceType);
            }

            public override IEnumerable<object> GetServices(Type serviceType)
            {
                return _kernel.GetAllInstances(serviceType).Concat(base.GetServices(serviceType));
            }
        }
    }
}
