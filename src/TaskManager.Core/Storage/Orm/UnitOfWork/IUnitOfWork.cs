using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Storage.Orm.UnitOfWork
{
    public interface IUnitOfWork
        : IDisposable
    {
        bool Committed { get; }
        bool Rolledback { get; }
        void Commit();
        void Rollback();
    }
}
