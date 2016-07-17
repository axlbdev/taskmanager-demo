using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TaskManagerDemo.Core.Platform.Context
{
    /// <summary>
    /// Хранилище на основе HttpContext.Current
    /// </summary>
    public class HttpContextBagProvider
        : IContextProvider
    {
        public static readonly string HttpContextBagKey = "_contextBagProvider";
        public IContext GetContext()
        {
            if(HttpContext.Current == null)
            {
                throw new InvalidOperationException("Current HttpContext is null");
            }
            if(HttpContext.Current.Items[HttpContextBagKey] == null)
            {
                HttpContext.Current.Items[HttpContextBagKey] = new ContextBag();
            }
            return (ContextBag)HttpContext.Current.Items[HttpContextBagKey];
        }


    }
}
