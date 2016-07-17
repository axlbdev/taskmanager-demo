using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

[assembly: OwinStartup(typeof(TaskManager.Core.Owin.Startup))]

namespace TaskManager.Core.Owin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureModelMapper(app);
            ConfigureContainer(app);
            ConfigureAuth(app);
            ConfigureWebApi(app);
            ConfigureSignalR(app);
        }
    }
}
