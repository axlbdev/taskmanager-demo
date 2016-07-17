using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Platform.Context
{
    /// <summary>
    /// Сохраняемый конекст
    /// </summary>
    public class ContextBag
            : IContext
    {
        private readonly Dictionary<string, object> _data
            = new Dictionary<string, object>();

        public object this[string key]
        {
            get
            {
                if (!_data.ContainsKey(key))
                {
                    return null;
                }
                return _data[key];
            }
            set
            {
                _data[key] = value;
            }
        }
    }
}
