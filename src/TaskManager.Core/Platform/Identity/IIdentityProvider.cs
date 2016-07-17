using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Platform.Identity
{
    /// <summary>
    /// Провайдер Identity
    /// </summary>
    public interface IIdentityProvider
    {
        /// <summary>
        /// Получить Identity текущего пользователя
        /// </summary>
        /// <returns></returns>
        UserIdentity GetCurrentIdentity();
    }
}
