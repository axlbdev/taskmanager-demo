using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TaskManagerDemo.Core.Mvc.Controllers
{
    /// <summary>
    /// Домашний контроллер
    /// </summary>
    public class HomeController
        : Controller
    {
        public ViewResult Index()
        {
            return new ViewResult();
        }
    }
}
