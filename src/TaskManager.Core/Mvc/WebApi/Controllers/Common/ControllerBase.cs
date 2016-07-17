using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManager.Core.Storage.Entities.Common;
using TaskManagerDemo.Core.Platform.Mapping;
using TaskManagerDemo.Core.Mvc.WebApi.ViewModels.Common;
using TaskManagerDemo.Core.Storage.Repositories.Common;

namespace TaskManagerDemo.Core.Mvc.WebApi.Controllers.Common
{
    /// <summary>
    /// Базовая релизация CRUD-контроллера
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности в хранилище</typeparam>
    /// <typeparam name="TViewModel">Тип для отображения карточки сущности</typeparam>
    /// <typeparam name="TListViewModel">Тип для отображения списка сущностей</typeparam>
    /// <typeparam name="TQueryArgument">Тип запроса для поиска</typeparam>
    public class ControllerBase<TEntity, TViewModel, TListViewModel, TQueryArgument>
        : ApiController
        where TViewModel: IEntityViewModel
        where TEntity : IEntity
    {
        protected readonly IRepository<TEntity> entityRepository;
        protected readonly IModelMapper mapper;
        public ControllerBase(IRepository<TEntity> entityRepository, IModelMapper mapper)
        {
            this.entityRepository = entityRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение сущности
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TViewModel Get(int id)
        {
            return mapper.Map<TEntity, TViewModel>(entityRepository.Get(id));
        }

        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="queryViewModel"></param>
        /// <returns></returns>
        public virtual QueryResultViewModel<TListViewModel> Get([FromUri] QueryViewModel<TQueryArgument> queryViewModel)
        {        
            var query = mapper.Map(queryViewModel.Query, entityRepository.Query().OrderByDescending(x=>x.Id));
            return new QueryResultViewModel<TListViewModel>
            {
                Count = 1,
                Page = mapper.ProjectTo<TEntity, TListViewModel>(query).ToArray()
            };
        }
        /// <summary>
        /// Изменение
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual TViewModel Post(TViewModel model)
        {
            var entity = entityRepository.GetOrCreate(model.Id);
            mapper.Map(model, entity);
            entityRepository.Save(entity);
            mapper.Map(entity, model);
            return model;
        }
        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(int id)
        {
            var entity = entityRepository.Get(id);
            entityRepository.Delete(entity);
        }
    }
}
