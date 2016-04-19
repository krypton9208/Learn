using Learn.Logger;
using Learn.Models;
using Learn.Repos.Abstract;
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
            log.WriteInfo("Employee/Index GetAction");
            return View(_repository.GetAll);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            log.WriteInfo("Employee/Add Get Action");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Employee model)
        {
            if (_repository.Add(model) >=0) 
                return RedirectToAction("Index");
            log.WriteError("Obiekt nie został dodany");
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View(_repository.GetById(id));
        }
        [Authorize(Roles = "Admin")]
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