using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NHibernate.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using TaskManager.Web.Models;
using TaskManagerDemo.Core.Platform.IoC;
using TaskManagerDemo.Core.Storage.Orm.UnitOfWork;
using TaskManagerDemo.Core.Storage.Orm.UnitOfWork.NHibernate;
using TaskManagerDemo.Core.Storage.Repositories.Common.NHibernate;

namespace TaskManager.Core.Owin
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public static ApplicationUserManager Create()
        {
            var uow = Container.Current.GetInstance<IUnitOfWork>() as UnitOfWork;
            if(uow == null)
            {
                throw new ConcreteUowRequiredException();
            }
                
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>( uow.Session ));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            return manager;
        }
    }
}
