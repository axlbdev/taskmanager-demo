using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Mvc.WebApi.ViewModels.Common
{
    /// <summary>
    /// Результат поиска
    /// </summary>
    /// <typeparam name="TListViewModel"></typeparam>
    public class QueryResultViewModel<TListViewModel>
    {
        public int Count { get; set; }
        public TListViewModel[] Page { get; set; }
    }
}
