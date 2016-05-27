using Learn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Repos.Abstract
{
    public interface IEmployeesRepository<T> : IRepository<T>
    {
        EmployeeViewModel Search(string search);
        EmployeeViewModel GetAllByCat(string cat);
        EmployeeViewModel Search(string search, string cat);
        EmployeeViewModel GetAllNew { get; }
    }
}
