using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using TaskManagerDemo.Core.Storage.Entities.Sockets;
using TaskManagerDemo.Core.Storage.Repositories.Common;
using TaskManagerDemo.Core.Platform.IoC;
using TaskManagerDemo.Core.Storage.Orm.UnitOfWork;


namespace TaskManagerDemo.Core.Platform.Sockets.SignalR
{
    /// <summary>
    /// Общий хаб
    /// </summary>
    public class CommonHub
        : Hub
    {
        private readonly IRepository<SocketConnection> _socketRepository;
        private readonly IRepository<TaskManager.Core.Storage.Entities.User> _userRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkfactory;


        public CommonHub(IUnitOfWorkFactory unitOfWorkfactory, IRepository<TaskManager.Core.Storage.Entities.User> userRepository, IRepository<SocketConnection> socketRepository)
        {
           _unitOfWorkfactory = unitOfWorkfactory;
           _socketRepository = socketRepository;
           _userRepository = userRepository;
        }

        public override Task OnConnected()
        {
            // На всякий случай сохраним кто подключен
            using (var uow = _unitOfWorkfactory.Create())
            {
                var cn = _socketRepository.GetOrCreate((int?)null);
                cn.ConnectionId = Context.ConnectionId;
                cn.User = _userRepository.Query()
                    .Where(x => x.Name == Context.User.Identity.Name)
                    .FirstOrDefault();
                _socketRepository.Save(cn);
                uow.Commit();
            }
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            // При отключении удалим запись
            using (var uow = _unitOfWorkfactory.Create())
            {
                var cns = _socketRepository.Query()
                    .Where(x => x.ConnectionId == Context.ConnectionId);
                foreach (var cn in cns)
                {
                    _socketRepository.Delete(cn);
                }
                uow.Commit();
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}
