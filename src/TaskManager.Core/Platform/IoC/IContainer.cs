using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Platform.IoC
{
    /// <summary>
    /// IoC- контейнер
    /// </summary>
    public interface IContainer
    {
        T GetInstance<T>();
        object ExposeImpl();
    }
}
