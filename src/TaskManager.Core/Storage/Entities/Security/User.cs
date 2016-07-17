using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Storage.Entities.Common;
using TaskManager.Web.Models;

namespace TaskManager.Core.Storage.Entities
{
    public class User
        : NamedEntityBase
    {
        public User()
        {
            Tasks = new List<Task>();
        }
        public virtual ApplicationUser Account { get; set; }
        public virtual IList<Task> Tasks { get; set; }
    }
}
