using FlightFolio.Business;
using FlightFolio.Models;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace FlightFolio.Controllers
{
    public class AeroplaneFolioController : Controller
    {
        private readonly AeroplaneFolioService _aeroplaneFolioService;

        public AeroplaneFolioController(AeroplaneFolioService aeroplaneFolioService)
        {
            _aeroplaneFolioService = aeroplaneFolioService ?? throw new ArgumentNullException(nameof(aeroplaneFolioService));
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10; // Adjust the page size as needed

            var aeroplaneFolios = await _aeroplaneFolioService.GetAeroplaneFoliosAsync();

            // Create a paged list from the aeroplaneFolios
            IPagedList<AeroplaneFolio> pagedList = aeroplaneFolios.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var aeroplaneFolio = await _aeroplaneFolioService.GetByIdAsync(id);
            if (aeroplaneFolio == null)
            {
                return NotFound();
            }
            return View(aeroplaneFolio);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AeroplaneFolio aeroplaneFolio)
        {
            if (ModelState.IsValid)
            {
                await _aeroplaneFolioService.AddAeroplaneFolioAsync(aeroplaneFolio);
                return RedirectToAction(nameof(Index));
            }
            return View(aeroplaneFolio);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var aeroplaneFolio = await _aeroplaneFolioService.GetByIdAsync(id);
            if (aeroplaneFolio == null)
            {
                return NotFound();
            }
            return View(aeroplaneFolio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AeroplaneFolio aeroplaneFolio)
        {
            if (id != aeroplaneFolio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _aeroplaneFolioService.UpdateAeroplaneFolioAsync(aeroplaneFolio);
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aeroplaneFolio);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var aeroplaneFolio = await _aeroplaneFolioService.GetByIdAsync(id);
            if (aeroplaneFolio == null)
            {
                return NotFound();
            }
            return View(aeroplaneFolio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _aeroplaneFolioService.DeleteAeroplaneFolioAsync(id);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
