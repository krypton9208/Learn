using Learn.Models.NHibernate;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Repos.Abstract
{
    public class Repository<T> : IRepository<T>, IDisposable
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        public Repository ()
        {
            _session = NHibernateSession.OpenSession();
        }

        public IEnumerable<T> GetAll
        {
            get
            {
                return _session.Query<T>().ToList();
            }
        }


        #region CRUD methods

        public T GetById(int t)
        {
            return _session.Get<T>(t);
        }

        public int Add(T t)
        {
            int id = (int)_session.Save(t);
            _session.Flush();
            return id;
        }

        public bool Delete(T t)
        {
            _session.Delete(t);
            try
            {
                _session.Flush();
            }
            catch
            {
                return false;
            }

            return true;
        }


        public T GetById(T objType, int t)
        {
            return (T)_session.Load(objType.GetType(), t);
        }


        public bool Update(T t)
        {
            _session.SaveOrUpdate(t);
            try
            {
                _session.Flush();
            }
            catch
            {
                return false;
            }
            return true;
        }

        #endregion

        #region IDisposable
        public void Dispose()
        {
            if (_transaction != null)
            {
                CommitTransaction();
            }

            if (_session != null)
            {
                _session.Flush();
                CloseSession();
            }
        }
        #endregion

        #region Transactions

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
            CloseTransaction();
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
            CloseTransaction();
            CloseSession();
        }

        private void CloseTransaction()
        {
            _transaction.Dispose();
            _transaction = null;
        }

        private void CloseSession()
        {
            _session.Close();
            _session.Dispose();
        }



        #endregion

    }
}
