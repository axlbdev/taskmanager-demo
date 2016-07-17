using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Platform.Context
{
    /// <summary>
    /// Провайдер конекста
    /// </summary>
    public interface IContextProvider
    {
        /// <summary>
        /// Получить текущий конекст
        /// </summary>
        /// <returns></returns>
        IContext GetContext();
    }
}
