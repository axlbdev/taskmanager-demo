using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Platform.Identity
{
    /// <summary>
    /// Identity пользователя
    /// </summary>
    public class UserIdentity
    {
        public static readonly UserIdentity Anonymous = new UserIdentity("");
        public UserIdentity(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }
}
