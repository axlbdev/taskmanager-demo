using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Mvc.WebApi.ViewModels.Common
{
    /// <summary>
    /// Сущность с названием
    /// </summary>
    public class NamedEntityViewModel
        : EntityViewModel
    {
        public string Name { get; set; }
    }
}
