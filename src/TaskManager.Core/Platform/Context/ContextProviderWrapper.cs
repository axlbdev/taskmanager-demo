using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TaskManagerDemo.Core.Platform.Context
{
    /// <summary>
    /// Обертка для LC и HTTP контекстов
    /// </summary>
    public class ContextProviderWrapper
        : IContextProvider
    {
        private IContextProvider httpProvider
             = new HttpContextBagProvider();
        private IContextProvider lcProvider
             = new CallContextBagProvider();
        /// <summary>
        /// Получить провайдер контекста. Если есть http.current, вернется http-провайдер, иначе - lc
        /// </summary>
        /// <returns>Провайдер контекста</returns>
        public IContext GetContext()
        {
            if(HttpContext.Current != null)
            {
                return httpProvider.GetContext();
            }
            return lcProvider.GetContext();
        }
    }
}
