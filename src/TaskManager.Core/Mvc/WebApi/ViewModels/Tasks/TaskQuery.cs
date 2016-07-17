using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Mvc.WebApi.ViewModels.Tasks
{
    /// <summary>
    /// Поисковый фильтр к задачам
    /// </summary>
    public class TaskQuery
    {
        public string Name { get; set; }
        public int? AuthorId { get; set; }
    }
}
