using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InterCityBus_MK.Data;
using InterCityBus_MK.Models;
using Microsoft.AspNetCore.Authorization;

namespace InterCityBus_MK.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new AdminDashboardViewModel
            {
                TotalStops = await _context.Stops.CountAsync(),
                TotalStations = await _context.Stations.CountAsync(),
                TotalTrips = await _context.Trips.CountAsync(),
                TotalCompanies = await _context.Companies.CountAsync()
            };

            return View(viewModel);
        }
    }
}