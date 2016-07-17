using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Storage.Entities.Common;

namespace TaskManagerDemo.Core.Storage.Repositories.Common
{
    public interface IRepository<TEntity>
        : IReadonlyRepository<TEntity>
        where TEntity: IEntity
    {
        TEntity GetOrCreate(int? id);
        void Save(TEntity entity);
        void Delete(TEntity entity);
    }
}
