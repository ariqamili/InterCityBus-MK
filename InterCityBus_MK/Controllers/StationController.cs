using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterCityBus_MK.Controllers
{
    public class StationController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: StationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                return View("Index");
            }
            return View();
        }

        // GET: StationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
