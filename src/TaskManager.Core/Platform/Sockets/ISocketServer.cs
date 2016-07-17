using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Platform.Sockets
{
    /// <summary>
    /// Интерфейс сокет-сервера
    /// </summary>
    public interface ISocketServer
    {
        /// <summary>
        /// Отправить сообщение
        /// </summary>
        /// <param name="type">Тип адресата</param>
        /// <param name="addressee">Адресат</param>
        /// <param name="message">Сообщение</param>
        void Send(AddresseeType type, string addressee, dynamic message);
    }
}
