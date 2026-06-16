using InterCityBus_MK.Data;
using InterCityBus_MK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InterCityBus_MK.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StopsController : Controller
    {
        private ApplicationDbContext _dbContext;

        public StopsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult> Index(CancellationToken ct)
        {
            var stops = await _dbContext.Stops
                .Include(s => s.Station)
                .ToListAsync(ct);

            return View(stops);
        }

        public ActionResult Create()
        {
            ViewData["StationId"] = new SelectList(_dbContext.Stations, "Id", "Name");
            ViewData["TripId"] = new SelectList(_dbContext.Trips, "Id", "Id"); 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Stop stop)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Stops.Add(stop);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["StationId"] = new SelectList(_dbContext.Stations, "Id", "Name", stop.StationId);
            ViewData["TripId"] = new SelectList(_dbContext.Trips, "Id", "Id", stop.TripId);
            return View(stop);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var stop = await _dbContext.Stops.FindAsync(id);
            if (stop == null)
            {
                return NotFound();
            }

            ViewData["StationId"] = new SelectList(_dbContext.Stations, "Id", "Name", stop.StationId);
            ViewData["TripId"] = new SelectList(_dbContext.Trips, "Id", "Id", stop.TripId);
            return View(stop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Stop stop)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Stops.Update(stop);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["StationId"] = new SelectList(_dbContext.Stations, "Id", "Name", stop.StationId);
            ViewData["TripId"] = new SelectList(_dbContext.Trips, "Id", "Id", stop.TripId);
            return View(stop);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var stop = await _dbContext.Stops
                .Include(s => s.Station)
                .Include(s => s.Trip)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (stop == null)
            {
                return NotFound();
            }
            return View(stop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Stop stop)
        {
            _dbContext.Stops.Remove(stop);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}