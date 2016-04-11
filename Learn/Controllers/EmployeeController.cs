using Learn.Logger;
using Learn.Models;
using Learn.Models.NHibernate;
using Learn.Repos.Abstract;
using log4net;
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
        private readonly ILoggerService<EmployeeController> log;

        public EmployeeController(IEmployeesRepository<Employee> repo, ILoggerService<EmployeeController> _log)
        {
            _repository = repo;
            log = _log;
        }

        public ActionResult Index()
        {
            log.WriteInfo("EmployeeIndex GetAction");
            return View(_repository.GetAll);
        }

        public ActionResult Add()
        {
            log.WriteInfo("Employee/Add Get Action");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Employee model)
        {
            if (_repository.Add(model) >=0) 
                return RedirectToAction("Index");
            log.WriteError("Obiekt nie został dodany");
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            return View(_repository.GetById(id));
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Employee model)
        {
            if (_repository.Delete(model))
               return RedirectToAction("Index");
            log.WriteError("Obiekt nie został usunięty.");
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(_repository.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee model)
        {
            if (_repository.Update(model))
                return RedirectToAction("Index");
            log.WriteError("Obiekt nie został zaaktualizowany.");
            return View(model);
        }
    }
}