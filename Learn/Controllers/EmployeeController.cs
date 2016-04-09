using Learn.Models;
using Learn.Models.NHibernate;
using Learn.Repos.Abstract;
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
        private readonly IRepository<Employee> _repository;

        public EmployeeController(IRepository<Employee> repo)
        {
            _repository = repo;
        }

        // GET: Employee

        public ActionResult Index()
        {
            return View(_repository.GetAll);
        }
    }
}