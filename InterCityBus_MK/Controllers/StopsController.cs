using InterCityBus_MK.Data;
using InterCityBus_MK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Required for SelectList
using Microsoft.EntityFrameworkCore;

namespace InterCityBus_MK.Controllers
{
    //[Authorize]
    public class StopsController : Controller
    {
        private ApplicationDbContext _dbContext;

        public StopsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult> Index(CancellationToken ct)
        {
            // We use Include to fetch the related data for display
            var stops = await _dbContext.Stops.ToListAsync(ct);

            return View(stops);
        }

        public ActionResult Create()
        {
            // Populate dropdowns for the view
            ViewData["StationId"] = new SelectList(_dbContext.Stations, "Id", "Name");
            ViewData["TripId"] = new SelectList(_dbContext.Trips, "Id", "Id"); // Using Id as display text for Trip, change if Trip has a name
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

            // If validation fails, reload the dropdowns so the form isn't empty
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

            // Populate dropdowns with the current selection
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

            // Reload dropdowns if validation fails
            ViewData["StationId"] = new SelectList(_dbContext.Stations, "Id", "Name", stop.StationId);
            ViewData["TripId"] = new SelectList(_dbContext.Trips, "Id", "Id", stop.TripId);
            return View(stop);
        }

        public async Task<ActionResult> Delete(int id)
        {
            // Include related data to show details on the Delete confirmation page
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