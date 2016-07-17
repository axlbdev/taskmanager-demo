using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Platform.Identity
{
    /// <summary>
    /// Провайдер на основе Thread.CurrentPrincipal
    /// </summary>
    public class ThreadPrincipalProvider
        : IIdentityProvider
    {
        public UserIdentity GetCurrentIdentity()
        {
            if(Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity != null && Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return new UserIdentity(Thread.CurrentPrincipal.Identity.Name);
            }
            return UserIdentity.Anonymous;
        }
    }
}
