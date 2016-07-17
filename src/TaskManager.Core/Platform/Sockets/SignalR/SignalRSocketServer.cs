using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDemo.Core.Storage.Entities.Sockets;
using TaskManagerDemo.Core.Storage.Repositories.Common;

namespace TaskManagerDemo.Core.Platform.Sockets.SignalR
{
    /// <summary>
    /// Реализация сокет-сервера на основе SignalR
    /// </summary>
    public class SignalRSocketServer
        : ISocketServer
    {
        private readonly IRepository<SocketConnection> _socketRepository;
        private readonly IHubConnectionContext<dynamic> _context;
        public SignalRSocketServer(IRepository<SocketConnection> socketRepository, IHubConnectionContext<dynamic> context/*, IDependencyResolver resolver*/)
        {
            _socketRepository = socketRepository;
            _context = context;
        }
        public void Send(AddresseeType type, string addressee, dynamic message)
        {
            switch(type)
            {
                case AddresseeType.User:
                    _context.User(addressee).receive(message);
                    break;
            }
        }
    }
}
