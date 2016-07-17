using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.AspNet.Identity.Helpers;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Web.Models;
using TaskManagerDemo.Core.Platform.Context;
using TaskManagerDemo.Core.Storage.Orm.Mappings;

namespace TaskManagerDemo.Core.Storage.Orm.UnitOfWork.NHibernate
{
    /// <summary>
    /// Фабрика UoW
    /// </summary>
    public class UnitOfWorkFactory
        : IUnitOfWorkFactory
    {
        #region Static
        public static readonly string ConnectionStringKey = "DefaultConnection";
        private static readonly string UnitContextKey = "_unitOfWork";

        private static Lazy<ISessionFactory> _sessionFactory = new Lazy<ISessionFactory>(CreateSessionFactory);

        protected static ISessionFactory sessionFactory
        {
            get
            {
                return _sessionFactory.Value;
            }
        }
        private static ISessionFactory CreateSessionFactory()
        {
            var identityEntities = new []
            {
                typeof(ApplicationUser)
            };

            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard
                    .ConnectionString(ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString.Replace("~",AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'))))
            .Mappings(x => x.AutoMappings.Add(
                AutoMap.Assembly(Assembly.GetExecutingAssembly(), new AutoMappingConfiguration())
                    .UseOverridesFromAssembly(Assembly.GetExecutingAssembly())))
            .ExposeConfiguration((config) => 
            {
                config.AddDeserializedMapping(MappingHelper.GetIdentityMappings(identityEntities), null);
                new SchemaUpdate(config).Execute(false, true);
            })
            .BuildSessionFactory();
        }
        #endregion
        private readonly IContextProvider contextProvider;
        public UnitOfWorkFactory(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public IUnitOfWork GetContextUnit()
        {
            var stack = getStack();
            return stack.Peek() as IUnitOfWork;
        }

        public IUnitOfWork Create()
        {
            var context = contextProvider.GetContext();
            var stack = getStack();
            var parent = stack.Count == 0 
                ? null 
                : stack.Peek();
            var unit = new UnitOfWork(parent, sessionFactory);
            unit.Disposing += unitDisposed;
            stack.Push(unit);
            return unit;
        }

        #region Private
        void unitDisposed(object sender, EventArgs e)
        {
            var unit = sender as UnitOfWork;
            var stackUnit = getStack().Peek();
            if(unit != stackUnit)
            {
                throw new InvalidOperationException("Non-hierarchy unit dispose called");
            }
        }

        private Stack<UnitOfWork>  getStack()
        {
            var context = contextProvider.GetContext();
            var stack = context[UnitContextKey] as Stack<UnitOfWork>;
            if (stack == null)
            {
                context[UnitContextKey] = stack = new Stack<UnitOfWork>();
            }
            return stack;
        }
        #endregion
    }
}
