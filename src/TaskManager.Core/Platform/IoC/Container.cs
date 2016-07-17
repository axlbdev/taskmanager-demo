using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Platform.IoC
{
    /// <summary>
    /// Реализация контейнера
    /// </summary>
    public class Container
        : IContainer
    {
        private static Lazy<Container> _current = new Lazy<Container>(() => new Container());

        private ServiceContainer _serviceContainer;
        private Container()
        {
            _serviceContainer = new ServiceContainer();
        }
        public static IContainer Current
        {
            get
            {
                return _current.Value;
            }
        }

        public T GetInstance<T>()
        {
            return _serviceContainer.GetInstance<T>();
        }


        public object ExposeImpl()
        {
            return _serviceContainer;
        }
    }
}
