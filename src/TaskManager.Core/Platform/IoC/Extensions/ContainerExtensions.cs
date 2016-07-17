using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Platform.IoC.Extensions
{
    public static class ContainerExtensions
    {
        /// <summary>
        /// Сконфигурировать контейнер
        /// </summary>
        /// <param name="container">Контейнер</param>
        /// <param name="implementationConfigure">Экшн на вход получает текущую реализацию контейнера</param>
        /// <returns></returns>
        public static IContainer Configure(this IContainer container, Action<object> implementationConfigure)
        {
            implementationConfigure(container.ExposeImpl());
            return container;
        }
    }
}
