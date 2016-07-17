using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Platform.Context
{
    /// <summary>
    /// Конекст
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// В реализации - получить объект контекста по ключу
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[string key] { get; set; }
    }
}
