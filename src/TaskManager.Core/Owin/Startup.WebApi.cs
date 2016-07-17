using LightInject;
using Newtonsoft.Json.Serialization;
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

namespace TaskManager.Core.Owin
{
    public partial class Startup
    {
        public void ConfigureWebApi(IAppBuilder app)
        {
            GlobalConfiguration.Configure((configuration) => 
                {
                    configuration.Filters.Add(Container.Current.GetInstance<UnitOfWorkAttribute>());
                    var jsonFormatter = configuration.Formatters.JsonFormatter;
                    var settings = jsonFormatter.SerializerSettings;
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
        }
    }
}
