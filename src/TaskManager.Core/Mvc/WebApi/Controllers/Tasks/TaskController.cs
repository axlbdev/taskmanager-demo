using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Core.Storage.Entities;
using TaskManagerDemo.Core.Platform.Mapping;
using TaskManagerDemo.Core.Mvc.WebApi.Controllers.Common;
using TaskManagerDemo.Core.Mvc.WebApi.ViewModels.Tasks;
using TaskManagerDemo.Core.Storage.Repositories.Common;
using TaskManagerDemo.Core.Mvc.WebApi.Controllers.Security;
using TaskManagerDemo.Core.Storage.Entities.Sockets;
using TaskManagerDemo.Core.Platform.Sockets;

namespace TaskManagerDemo.Core.Mvc.WebApi.Controllers.Tasks
{
    /// <summary>
    /// Работа с задачами
    /// </summary>
    public class TaskController
        : ControllerBase<Task, TaskViewModel,  TaskViewModel,  TaskQuery>
    {
        private readonly UserController _userController;
        private readonly ISocketServer _socketServer;
        public TaskController(IRepository<Task> entityRepository, IModelMapper mapper, UserController userController, ISocketServer socketServer)
            : base(entityRepository, mapper) 
        {
            _userController = userController;
            _socketServer = socketServer;
        }

        /// <summary>
        /// Поиск по автору и названию
        /// </summary>
        /// <param name="queryViewModel"></param>
        /// <returns></returns>
        public override ViewModels.Common.QueryResultViewModel<TaskViewModel> Get(ViewModels.Common.QueryViewModel<TaskQuery> queryViewModel)
        {
            //Сложных правил авторизации пока нет, проверим в теле метода. 
            //TODO: Авторизация атрибутами
            var current = _userController.Current();
            if(current == null)
            {
                return null;
            }
            if(queryViewModel.Query == null)
            {
                queryViewModel.Query = new TaskQuery();
            }
            queryViewModel.Query.AuthorId = current.Id;
            return base.Get(queryViewModel);
        }
        /// <summary>
        /// Сохранение с уведомлением других открытых пользователем клиентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override TaskViewModel Post(TaskViewModel model)
        {
            var isNew = !model.Id.HasValue;
            //Сложных правил авторизации пока нет, проверим в теле метода. 
            //TODO: Авторизация атрибутами
            var current = _userController.Current();
            if(current == null)
            {
                throw new InvalidOperationException();
            }
            if(!isNew)
            {
                var entity = entityRepository.Get(model.Id.Value);
                if(entity.Author.Id != current.Id)
                {
                    throw new InvalidOperationException();
                }
            }
            model.AuthorId = current.Id;
            
            base.Post(model);

            _socketServer.Send(AddresseeType.User, current.Name, new 
            { 
                scope = "task", 
                type = isNew 
                    ?  "new" 
                    : "update", 
                payload = new 
                { 
                    id = model.Id, 
                    name = model.Name 
                } 
            });

            return model;
        }
        /// <summary>
        /// Удаление с уведомлением других открытых пользователем клиентов
        /// </summary>
        /// <param name="id"></param>
        public override void Delete(int id)
        {
            //Сложных правил авторизации пока нет, проверим в теле метода. 
            //TODO: Авторизация атрибутами
            var current = _userController.Current();

            if (current == null)
            {
                throw new InvalidOperationException();
            }
            var entity = entityRepository.Get(id);
            if (entity.Author.Id != current.Id)
            {
                throw new InvalidOperationException();
            }

            _socketServer.Send(AddresseeType.User, current.Name, new 
            { 
                scope = "task", 
                type = "delete", 
                payload = new { id = id } 
            });
            base.Delete(id);
        }
    }
}
