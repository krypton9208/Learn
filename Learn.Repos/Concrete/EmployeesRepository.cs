using Learn.Models;
using Learn.Repos.Abstract;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Repos.Concrete
{
    public class EmployeesRepository : IRepository<Employee>, IDisposable
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        public EmployeesRepository(ISession session)
        {
            _session = session;
        }
       
        public IEnumerable<Employee> GetAll
        {
            get
            {
                return _session.Query<Employee>().ToList();
            }
        }


        #region CRUD methods

        public int Add(Employee t)
        {
            int id = (int)_session.Save(t);
            _session.Flush();
            return id;
        }

        public bool Delete(Employee t)
        {
            _session.Delete(t);
            try
            {
                _session.Flush();
            }
            catch (IOException e)
            {
                return false;
            }

            return true;
        }


        public Employee GetById(Employee objType, int t)
        {
            return (Employee)_session.Load(objType.GetType(), t);
        }


        public bool Update(Employee t)
        {
            _session.SaveOrUpdate(t);
            try
            {
                _session.Flush();
            }
            catch (IOException e)
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

        public Employee GetById(Type objType, int t)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
