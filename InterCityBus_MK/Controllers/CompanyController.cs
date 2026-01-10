using InterCityBus_MK.Data;
using InterCityBus_MK.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterCityBus_MK.Controllers
{
    public class CompanyController : Controller
    {
        private ApplicationDbContext _dbContext;

        public CompanyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var companies = _dbContext.Companies.ToList();
            return View(companies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Companies.Add(company);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        public IActionResult Edit(int id)
        {
            var company = _dbContext.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Companies.Update(company);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        public IActionResult Delete(int id)
        {
            var company = _dbContext.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Company company)
        {
            _dbContext.Companies.Remove(company);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
