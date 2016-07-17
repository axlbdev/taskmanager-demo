using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Storage.Entities.Common
{
    public abstract class NamedEntityBase
        : EntityBase
    {
        public virtual string Name { get; set; }
    }
}
