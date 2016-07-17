using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDemo.Core.Storage.Orm.Mappings;
using System.Diagnostics;

namespace TaskManagerDemo.Core.Storage.Orm.UnitOfWork.NHibernate
{
    public class UnitOfWork
        : IUnitOfWork
    {

        private bool _commited;
        private bool _rolledback;

        private UnitOfWork _parent;
        internal UnitOfWork(UnitOfWork parent, ISessionFactory sessionFactory)
        {
            _parent = parent;
            if (_parent == null)
            {
                Debug.WriteLine("UOW: Create");
                Session = sessionFactory.OpenSession();
                _transaction = Session.BeginTransaction();
            } else
            {
                Session = _parent.Session;
            }
        }

        public ISession Session { get; private set; }
        private ITransaction _transaction;

        public void Commit()
        {
            if (_rolledback)
            {
                throw new InvalidOperationException("UoW was rolledback.");
            }
            if(_parent != null)
            {
                _parent.Commit();
            }
            _commited = true;
        }

        public void Rollback()
        {
            if(_commited)
            {
                throw new InvalidOperationException("UoW was committed.");
            }
            if (_parent != null)
            {
                _parent.Rollback();
            }
            _rolledback = true;
        }


        public event EventHandler Disposing;

        public void Dispose()
        {
            Disposing(this, EventArgs.Empty);

            if(_parent != null)
            {
                return;
            }
            try
            {
                Debug.WriteLine("UOW: Dispose");
                if (_transaction != null && _transaction.IsActive)
                {
                    if (_commited)
                    {
                        _transaction.Commit();
                    }
                    else
                    {
                        _transaction.Rollback();
                    }
                }
            } finally
            {
                if (Session != null)
                {
                    Session.Dispose();
                }
            }
        }

        public bool Committed
        {
            get { return _commited; }
        }

        public bool Rolledback
        {
            get { return _rolledback; }
        }
    }
}
