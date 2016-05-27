using Learn.App_Start;
using Learn.Language;
using Learn.Logger;
using Learn.Models;
using Learn.Repos.Abstract;
using System.Web.Mvc;

namespace Learn.Controllers
{
    [Authorize(Roles = "User")]
    [RoutePrefix("Employee")]
    public class EmployeeController : Controller
    {
        
        private readonly ILearnUserRepostiory<LearnUser> _userRepo;
        private readonly IEmployeesRepository<Employee> _repository;
        //private readonly ILoggerService<EmployeeController> log;

        public EmployeeController(IEmployeesRepository<Employee> repo, ILearnUserRepostiory<LearnUser> userRepo)
        {
            _repository = repo;
            _userRepo = userRepo;
        }
        
        public ActionResult Index()
        {
            //log.WriteInfo("Employee/Index GetAction");
            return View(_repository.GetAllNew);
        }


        [HttpPost]
        public ActionResult Index(string search, string Categories)
        {
            if (Categories != string.Empty && search != string.Empty)
            {
                return View(_repository.Search(search, Categories));
            }
            else if (Categories == string.Empty && search != string.Empty)
            {
                return View(_repository.Search(search));
            }
            else if (Categories != string.Empty && search == string.Empty)
            {
                return View(_repository.GetAllByCat(Categories));
            }
            else
            return View(_repository.Search(search));
            //log.WriteInfo("Employee/Index SearchAction");
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
        public ActionResult Home()
        {
            return RedirectToAction("Index");
        }

        public ActionResult AllowUser()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AllowUser(string login)
        {
            _userRepo.AllowToAccess(login);
            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            //log.WriteInfo("Employee/Add Get Action");
            return View();
        }
        public ActionResult Details(int id)
        {
            return View(_repository.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Employee model)
        {
            if (_repository.Add(model) >=0) 
                return RedirectToAction("Index");
            //log.WriteError("Obiekt nie został dodany");
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
            //log.WriteError("Obiekt nie został usunięty.");
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
            //log.WriteError("Obiekt nie został zaaktualizowany.");
            return View(model);
        }
    }
}