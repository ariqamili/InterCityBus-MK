using InterCityBus_MK.Data;
using InterCityBus_MK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterCityBus_MK.Controllers
{
    //[Authorize]
    public class StationController : Controller
    {
        private ApplicationDbContext _dbContext;

        public StationController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ActionResult Index()
        {
            var stations = _dbContext.Stations.ToList();
            return View(stations);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Station station)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Stations.Add(station);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(station);
        }

        public ActionResult Edit(int id)
        {
            var station = _dbContext.Stations.Find(id);
            if (station == null)
            {
                return NotFound();
            }
            return View(station);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Station station)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Stations.Update(station);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(station);
        }

        public ActionResult Delete(int id)
        {
            var station = _dbContext.Stations.Find(id);
            if (station == null)
            {
                return NotFound();
            }
            return View(station);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Station station)
        {
            _dbContext.Stations.Remove(station);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
