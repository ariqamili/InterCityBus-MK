using InterCityBus_MK.Data;
using InterCityBus_MK.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterCityBus_MK.Controllers
{
    public class TripController : Controller
    {
        private ApplicationDbContext _dbContext;

        public TripController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var tripViewModels = await _dbContext.Trips
        .Select(trip => new TripDisplayViewModel
        {
            Id = trip.Id,
            CompanyName = trip.Company.Name,
            FromStationName = trip.FromStation.Name,
            ToStationName = trip.ToStation.Name,
            DepartureTime = trip.DepartureTime,
            ArrivalTime = trip.ArrivalTime,
            Price = trip.Price
        }).ToListAsync(ct);
            return View(tripViewModels);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new TripFormViewModel {};
            await PopulateList(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TripFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var trip = new Models.Trip
                {
                    CompanyId = viewModel.CompanyId,
                    FromStationId = viewModel.FromStationId,
                    ToStationId = viewModel.ToStationId,
                    DepartureTime = viewModel.DepartureTime,
                    ArrivalTime = viewModel.ArrivalTime,
                    Price = viewModel.Price
                };
                _dbContext.Trips.Add(trip);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await PopulateList(viewModel);
            return View(viewModel);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var trip = await _dbContext.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            var viewModel = new TripFormViewModel
            {
                Id = trip.Id,
                CompanyId = trip.CompanyId,
                FromStationId = trip.FromStationId,
                ToStationId = trip.ToStationId,
                DepartureTime = trip.DepartureTime,
                ArrivalTime = trip.ArrivalTime,
                Price = trip.Price,
            };
            await PopulateList(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TripFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var trip = await _dbContext.Trips.FindAsync(viewModel.Id);
                if (trip == null)
                {
                    return NotFound();
                }

                trip.CompanyId = viewModel.CompanyId;
                trip.FromStationId = viewModel.FromStationId;
                trip.ToStationId = viewModel.ToStationId;
                trip.DepartureTime = viewModel.DepartureTime;
                trip.ArrivalTime = viewModel.ArrivalTime;
                trip.Price = viewModel.Price;
                _dbContext.Trips.Update(trip);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await PopulateList(viewModel);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var trip = await _dbContext.Trips
            .Include(t => t.Company)
            .Include(t => t.FromStation)
            .Include(t => t.ToStation)
            .Include(t => t.Stops).ThenInclude(s => s.Station)
            .FirstOrDefaultAsync(t => t.Id == id);

            if (trip == null)
            {
                return NotFound();
            }

            var viewModel = new TripDisplayViewModel
            {
                Id = trip.Id,
                CompanyName = trip.Company.Name,
                FromStationName = trip.FromStation.Name,
                ToStationName = trip.ToStation.Name,
                DepartureTime = trip.DepartureTime,
                ArrivalTime = trip.ArrivalTime,
                Price = trip.Price,
                AllStops = trip.Stops.OrderBy(s => s.StopOrder).Select(s => new StopDisplayViewModel
                {
                    StationName = s.Station.Name,
                    ArrivalTime = s.ArrivalTime,
                    StopOrder = s.StopOrder
                }).ToList()
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var trip = await _dbContext.Trips.FindAsync(id);
            if(trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TripFormViewModel viewModel)
        {
            var trip = await _dbContext.Trips.FindAsync(viewModel.Id);
            if (trip == null)
            {
                return NotFound();
            }
            _dbContext.Trips.Remove(trip);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateList(TripFormViewModel viewModel)
        {
            viewModel.Companies = await _dbContext.Companies.ToListAsync();
            viewModel.Stations = await _dbContext.Stations.ToListAsync();
        }
    }
}
