using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Storage.Entities.Common;

namespace TaskManagerDemo.Core.Storage.Repositories.Common
{
    public interface IReadonlyRepository<TEntity>
        where TEntity: IEntity
    {
        IQueryable<TEntity> Query();
        TEntity Get(int id);
    }
}
