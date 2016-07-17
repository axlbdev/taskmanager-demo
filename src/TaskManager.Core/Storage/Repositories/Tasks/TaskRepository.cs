using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Core.Storage.Entities;
using TaskManagerDemo.Core.Storage.Orm.UnitOfWork;
using TaskManagerDemo.Core.Storage.Repositories.Common.NHibernate;
namespace TaskManagerDemo.Core.Storage.Repositories.Tasks
{
    public class TaskRepository
        : RepositoryBase<Task>
    {
        public TaskRepository(Lazy<IUnitOfWork> uow)
            : base(uow) { }
    }
}
