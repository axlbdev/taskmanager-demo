using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using TaskManagerDemo.Core.Mvc.WebApi.ViewModels.Common;

namespace TaskManagerDemo.Core.Mvc.WebApi.Binders
{
    /// <summary>
    /// Биндер запросов к данным, приходит в "разложенном" виде параметров GET-запроса
    /// </summary>
    public class QueryViewModelBinder : IModelBinder
    {
        private static ConcurrentDictionary<Type, Func<object, object[], object>> QuerysetterCache = new ConcurrentDictionary<Type, Func<object, object[], object>>();
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {

            var result = Activator.CreateInstance(bindingContext.ModelMetadata.ModelType);
            var pageable = result as  PageableQueryViewModel;
            if(result == null)
            {
                return false;
            }
            var takeValue = bindingContext.ValueProvider.GetValue("take");
            var skipValue = bindingContext.ValueProvider.GetValue("skip");
            var queryValue = bindingContext.ValueProvider.GetValue("query");
            if(takeValue != null)
            {
                pageable.Take = int.Parse(takeValue.AttemptedValue);
            }
            if(skipValue != null)
            { 
                pageable.Skip = int.Parse(skipValue.AttemptedValue);
            }
            if(queryValue != null)
            {
                var query = Newtonsoft.Json.JsonConvert.DeserializeObject(queryValue.AttemptedValue, bindingContext.ModelMetadata.ModelType.GetGenericArguments().First());
                if(query != null)
                {
                    Func<object, object[], object> setter = null;
                    if(!QuerysetterCache.TryGetValue(bindingContext.ModelMetadata.ModelType, out setter))
                    {
                        setter = bindingContext.ModelMetadata.ModelType.GetProperty("Query").GetSetMethod().Invoke;
                        QuerysetterCache.TryAdd(bindingContext.ModelMetadata.ModelType, setter);
                    }
                    setter(result, new[] { query });
                }
            }


            bindingContext.Model = result;
            

            return true;
        }
    }
}
