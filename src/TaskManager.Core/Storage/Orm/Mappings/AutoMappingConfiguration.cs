using FluentNHibernate.Automapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Storage.Entities.Common;

namespace TaskManagerDemo.Core.Storage.Orm.Mappings
{
    public class AutoMappingConfiguration
        : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return typeof(IEntity).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract;
        }
    }
}
