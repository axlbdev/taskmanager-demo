using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Storage.Repositories.Common.NHibernate
{
    public class ConcreteUowRequiredException
        : InvalidOperationException
    {
        public ConcreteUowRequiredException()
            : base("Nhibernate UnitOfWork implementation expected.") { }
    }
}
