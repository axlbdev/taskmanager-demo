using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Storage.Entities;
using TaskManagerDemo.Core.Storage.Orm.UnitOfWork;
using TaskManagerDemo.Core.Storage.Repositories.Common.NHibernate;

namespace TaskManagerDemo.Core.Storage.Repositories.Security
{
    public class UserRepository
        : RepositoryBase<User>
    {
        public UserRepository(Lazy<IUnitOfWork> uow)
            : base(uow) { }
    }
}
