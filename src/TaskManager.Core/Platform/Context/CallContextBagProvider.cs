using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Platform.Context
{
    /// <summary>
    /// Хранилище на основе LogicalCallContext
    /// </summary>
    public class CallContextBagProvider
        : IContextProvider
    {
        public static readonly string HttpContextBagKey = "_contextBagProvider";
        public IContext GetContext()
        {
            var bag = CallContext.LogicalGetData(HttpContextBagKey);
            if (bag == null)
            {
                bag = new ContextBag();
                CallContext.LogicalSetData(HttpContextBagKey, bag);
            }
            return (ContextBag)bag;
        }
    }
}
