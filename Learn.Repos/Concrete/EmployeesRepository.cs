using Learn.Models;
using Learn.Models.NHibernate;
using Learn.Repos.Abstract;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Learn.Repos.Concrete
{
    public class EmployeesRepository : Repository<Employee>, IEmployeesRepository<Employee>
    {
        private readonly ISession _session;

        public EmployeesRepository()
        {
            _session = NHibernateSession.OpenSession();
        }

        public EmployeeViewModel GetAllNew
        {
            get
            {
                var db = _session.Query<Employee>().ToList();
                var t = new EmployeeViewModel();
                t.Categories = new HashSet<SelectListItem>();
                t.Model = new HashSet<Employee>();
                t.Categories = GetCatAll;
                foreach (var item in db)
                {
                    t.Model.Add(item);
                }
                return t;
            }

        }


        public EmployeeViewModel GetAllByCat(string cat)
        {
            var db = _session.Query<Employee>().ToList();
            var t = new EmployeeViewModel();
            t.Categories = new HashSet<SelectListItem>();
            t.Categories = GetCatAll;
            t.Model = new HashSet<Employee>();
            if (db.Count > 0)
            {
                foreach (var item in db)
                {
                    if (item.Category == cat)
                    {
                        t.Model.Add(item);
                    }
                }
                return t;
            }
            return new EmployeeViewModel() { Categories = GetCatAll };
        }

        public EmployeeViewModel Search(string search)
        {
            var t = _session.Query<Employee>().ToList();
            var r = new EmployeeViewModel();
            r.Categories = GetCatAll;
            foreach (var item in t)
            {
                if (item.Name.ToLower().Contains(search.ToLower()))
                {
                    r.Model.Add(item);
                }
            }
            return r;

        }

        private ICollection<SelectListItem> GetCatAll
        {
            get
            {
                var d = new HashSet<SelectListItem>();
                var t = _session.Query<Employee>().ToList();
                d.Add(new SelectListItem { Text = string.Empty, Value = string.Empty });
                foreach (var item in t)
                {
                    if (d.Where(x => x.Text == item.Category).Count() == 0)
                    {
                        d.Add(new SelectListItem { Text = item.Category, Value = item.Category });
                    }
                }
                return d;


            }
        }
        public EmployeeViewModel Search(string search, string cat)
        {
            var t = GetAllByCat(cat);
            var y = new EmployeeViewModel();
            y.Categories = GetCatAll;
            if (t.Model.Count > 0)
            {
                foreach (var item in t.Model)
                {
                    if (item.Name.ToLower().Contains(search.ToLower()))
                    {
                        y.Model.Add(item);
                    }
                }
                if (y.Model.Count > 0)
                    return y;
                return new EmployeeViewModel() { Categories = GetCatAll};

            }
            return new EmployeeViewModel() { Categories = GetCatAll };
        }


    }
}
