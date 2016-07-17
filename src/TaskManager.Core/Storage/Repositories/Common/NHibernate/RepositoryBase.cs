using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDemo.Core.Storage.Orm.UnitOfWork;
using TaskManagerDemo.Core.Storage.Orm.UnitOfWork.NHibernate;
using NHibernate.Linq;
using TaskManager.Core.Storage.Entities.Common;

namespace TaskManagerDemo.Core.Storage.Repositories.Common.NHibernate
{
    public class RepositoryBase<TEntity>
        : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly Lazy<IUnitOfWork> _uow;
        protected Lazy<UnitOfWork> uow;
        public RepositoryBase(Lazy<IUnitOfWork> uow1)
        {
            this._uow = uow1;
            this.uow = new Lazy<UnitOfWork>(CreateUnitOfWork);
        }
        public TEntity GetOrCreate(int? id)
        {
            if(id.HasValue)
            {
                return Get(id.Value);
            }
            var entity = new TEntity();
            return entity;
        }

        public IQueryable<TEntity> Query()
        {
            return uow.Value.Session.Query<TEntity>();
        }

        public TEntity Get(int id)
        {
            return uow.Value.Session.Get<TEntity>(id);
        }

        public void Save(TEntity entity)
        {
            uow.Value.Session.SaveOrUpdate(entity);

        }
        public void Delete(TEntity entity)
        {
            uow.Value.Session.Delete(entity);
        }
        private UnitOfWork CreateUnitOfWork()
        {
            var result = _uow.Value as UnitOfWork;
            if (result == null)
            {
                throw new ConcreteUowRequiredException();
            }
            return result;
        }
    }
}
