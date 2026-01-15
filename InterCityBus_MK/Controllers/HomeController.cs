using System.Diagnostics;
using InterCityBus_MK.Data;
using InterCityBus_MK.Models;
using InterCityBus_MK.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterCityBus_MK.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new TripSearchViewModel();
            await PopulateStations(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TripSearchViewModel viewModel)
        {
            await PopulateStations(viewModel);
            if (ModelState.IsValid)
            {
                viewModel.Results = await _dbContext.Trips
                    .Where(t => t.FromStationId == viewModel.FromStationId && t.ToStationId == viewModel.ToStationId)
                    .OrderBy(t => t.DepartureTime)
                    .Select(
                    trip => new TripDisplayViewModel
                    {
                        Id = trip.Id,
                        CompanyName = trip.Company.Name,
                        FromStationName = trip.FromStation.Name,
                        ToStationName = trip.ToStation.Name,
                        DepartureTime = trip.DepartureTime,
                        ArrivalTime = trip.ArrivalTime,
                        Price = trip.Price
                    }).ToListAsync();
                viewModel.HasSearched = true;

                if (viewModel.Results.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, "No trips found for the selected criteria.");
                }
                return View(viewModel);
            }
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task PopulateStations(TripSearchViewModel viewModel)
        {
            viewModel.Stations = await _dbContext.Stations
                .OrderBy(s => s.City)
                .ThenBy(s => s.Name)
                .ToListAsync();
        }
    }
}
