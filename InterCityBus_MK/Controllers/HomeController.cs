using System.Diagnostics;
using System.Linq.Expressions;
using InterCityBus_MK.Data;
using InterCityBus_MK.Models;
using InterCityBus_MK.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

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
                var dateToday = DateOnly.FromDateTime(DateTime.Now);
                var timeNow = TimeOnly.FromDateTime(DateTime.Now);
                if (viewModel.TravelDate == dateToday || viewModel.TravelDate == null)
                {
                    await TripSearch(viewModel, trip =>
                        trip.DepartureTime >= timeNow);
                }
                else if (viewModel.TravelDate > dateToday)
                {
                    await TripSearch(viewModel, trip => true);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Travel date cannot be in the past.");
                }
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

        private async Task TripSearch(TripSearchViewModel viewModel, Expression<Func<Trip, bool>> timeComparison)
        {
            var matchingTrips = await _dbContext.Stops.Where(s => s.StationId == viewModel.FromStationId)
                .Join(_dbContext.Stops.Where(s => s.StationId == viewModel.ToStationId), 
                fromStop => fromStop.TripId,
                toStop => toStop.TripId,
                (fromStop, toStop) => new
                {
                    TripId = fromStop.TripId,
                    FromStopOrder = fromStop.StopOrder,
                    ToStopOrder = toStop.StopOrder,
                    DepartureTime = fromStop.ArrivalTime,
                    ArrivalTime = toStop.ArrivalTime,
                    FromStationName = fromStop.Station.Name,
                    ToStationName = toStop.Station.Name
                }
                )
                .Where(stops => stops.FromStopOrder < stops.ToStopOrder)
                .ToListAsync();

            var tripIds = matchingTrips.Select(t => t.TripId)
                .Distinct()
                .ToList();

            var trips = await _dbContext.Trips
                        .Include(t => t.Company)
                        .Where(trip => tripIds.Contains(trip.Id))
                        .Where(timeComparison)
                        .ToListAsync();

            viewModel.Results = trips
                        .Join(matchingTrips,
                            trip => trip.Id,
                            times => times.TripId,
                            (trip, times) => new TripDisplayViewModel
                            {
                                Id = trip.Id,
                                CompanyName = trip.Company.Name,
                                FromStationName = times.FromStationName,
                                ToStationName = times.ToStationName,
                                DepartureTime = times.DepartureTime,
                                ArrivalTime = times.ArrivalTime,
                                Price = trip.Price
                            }
                        )
                        .OrderBy(t => t.DepartureTime)
                        .ToList();

            viewModel.HasSearched = true;

            if (viewModel.Results.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "No trips found for the selected criteria.");
            }
        }
    }
}
