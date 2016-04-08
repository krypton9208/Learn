using Learn.Models;
using Learn.Models.NHibernate;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                var persons = session.Query<Employee>().ToList();
                return View(persons);
            }
        }
    }
}