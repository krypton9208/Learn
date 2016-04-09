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
        private readonly IEmployeesRepository<Employee> _repository;

        public EmployeeController(IEmployeesRepository<Employee> repo)
        {
            _repository = repo;
        }

        public ActionResult Index()
        {
            return View(_repository.GetAll);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Employee model)
        {
            if (_repository.Add(model) >0)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}