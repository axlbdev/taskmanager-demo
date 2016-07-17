using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using TaskManagerDemo.Core.Storage.Orm.UnitOfWork;

namespace TaskManagerDemo.Core.Mvc.WebApi.Filters
{
    /// <summary>
    /// Фильтр стартует UoW в начале запроса и завершает его в кнце запроса
    /// </summary>
    public class UnitOfWorkAttribute
        : ActionFilterAttribute
    
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        public UnitOfWorkAttribute(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var unit = unitOfWorkFactory.Create();
            base.OnActionExecuting(actionContext);
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            var unit = unitOfWorkFactory.GetContextUnit();
            if(unit != null)
            {
                if(actionExecutedContext.Exception == null)
                {
                    unit.Commit();
                }
                else
                {
                    unit.Rollback();
                }
                unit.Dispose();
            }
        }
    }
}
