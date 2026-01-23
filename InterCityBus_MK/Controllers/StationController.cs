using InterCityBus_MK.Data;
using InterCityBus_MK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterCityBus_MK.Controllers
{
    [Authorize]
    public class StationController : Controller
    {
        private ApplicationDbContext _dbContext;

        public StationController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ActionResult> Index(CancellationToken ct)
        {
            var stations = await _dbContext.Stations.ToListAsync(ct);
            return View(stations);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Station station)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Stations.Add(station);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(station);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var station = await _dbContext.Stations.FindAsync(id);
            if (station == null)
            {
                return NotFound();
            }
            return View(station);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Station station)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Stations.Update(station);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(station);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var station = await _dbContext.Stations.FindAsync(id);
            if (station == null)
            {
                return NotFound();
            }
            return View(station);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Station station)
        {
            _dbContext.Stations.Remove(station);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
