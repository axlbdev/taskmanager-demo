using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDemo.Core.Mvc.WebApi.ViewModels.Common;

namespace TaskManagerDemo.Core.Mvc.WebApi.ViewModels.Tasks
{
    /// <summary>
    /// Вьюмодель задачи
    /// </summary>
    public class TaskViewModel
        : NamedEntityViewModel
    {
        /// <summary>
        /// Идентификатор автора задачи
        /// </summary>
        public int? AuthorId { get; set; }
    }
}
