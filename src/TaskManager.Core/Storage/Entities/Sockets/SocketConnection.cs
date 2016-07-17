using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Storage.Entities;
using TaskManager.Core.Storage.Entities.Common;

namespace TaskManagerDemo.Core.Storage.Entities.Sockets
{
    public class SocketConnection
        : EntityBase
    {
        public virtual string ConnectionId { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual User User { get; set; }
    }
}
