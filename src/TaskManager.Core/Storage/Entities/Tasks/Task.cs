using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Storage.Entities.Common;

namespace TaskManager.Core.Storage.Entities
{
    public class Task
        : NamedEntityBase
    {
        public virtual User Author { get; set; }
    }
}
