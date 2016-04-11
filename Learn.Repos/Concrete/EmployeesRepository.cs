using Learn.Models;
using Learn.Models.NHibernate;
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
    public class EmployeesRepository : Repository<Employee>, IEmployeesRepository<Employee>
    {
        private readonly ISession _session;

        public EmployeesRepository()
        {
            _session = NHibernateSession.OpenSession();
        }

      
    }
}
