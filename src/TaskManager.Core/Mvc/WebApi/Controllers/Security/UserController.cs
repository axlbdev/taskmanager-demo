using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManager.Core.Storage.Entities;
using TaskManagerDemo.Core.Platform.Identity;
using TaskManagerDemo.Core.Storage.Repositories.Common;

namespace TaskManagerDemo.Core.Mvc.WebApi.Controllers.Security
{
    /// <summary>
    /// Контроллер управления пользователеми
    /// </summary>
    public class UserController
        : ApiController
    {
        private readonly IRepository<User> _userRepository;
        private readonly IIdentityProvider _identityProvider;
        public UserController(IRepository<User> userRepository, IIdentityProvider identityProvider)
        {
            _userRepository = userRepository;
            _identityProvider = identityProvider;
        }

        /// <summary>
        /// Возвращает текущего пользователя. Если пользователь анонимный, вернет null
        /// </summary>
        /// <returns>Текущей пользователь. Если пользователь анонимный, то null</returns>
        public User Current()
        {
            var identity = _identityProvider.GetCurrentIdentity();
            if(identity == UserIdentity.Anonymous)
            {
                return null;
            }
            return _userRepository.Query()
                .Where(x => x.Name == identity.Name)
                .FirstOrDefault();
        }
    }
}
