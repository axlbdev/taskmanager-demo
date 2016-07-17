using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using TaskManagerDemo.Core.Mvc.WebApi.Binders;

namespace TaskManagerDemo.Core.Mvc.WebApi.ViewModels.Common
{
    /// <summary>
    /// Поисковый запрос
    /// </summary>
    /// <typeparam name="TQueryArgument"></typeparam>
    [ModelBinder(typeof(QueryViewModelBinder))]
    public class QueryViewModel<TQueryArgument>
        : PageableQueryViewModel
    {
        public TQueryArgument Query { get; set; }
    }
    /// <summary>
    /// Пейджинговый запрос
    /// </summary>
    public class PageableQueryViewModel
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
