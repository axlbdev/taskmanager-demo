using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Mvc.WebApi.ViewModels.Common
{
    /// <summary>
    /// Вьюмодель
    /// </summary>
    public class EntityViewModel
        : IEntityViewModel
    {
        public int? Id { get; set; }
    }
}
