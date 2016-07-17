using System.Web;
using System.Web.Mvc;

namespace TaskManager.Core.Owin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
