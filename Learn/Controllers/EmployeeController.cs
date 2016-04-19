using Learn.App_Start;
using Learn.Language;
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
        private readonly ILanguage lang;

        public EmployeeController(IEmployeesRepository<Employee> repo, ILoggerService<EmployeeController> _log, ILanguage _lang)
        {
            _repository = repo;
            log = _log;
            lang = _lang;
        }
        
        public ActionResult Index()
        {
           
            ViewBag.Logout = lang.GetResourceLogout;
            log.WriteInfo("Employee/Index GetAction");
            return View(_repository.GetAll);
        }

        public ActionResult ChangeLanguage()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeLanguage(string Language)
        {
            GlobalizeFilterAttribute.SavePreferredCulture(HttpContext.Response, Language, 30);
            return RedirectToAction("Index");
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