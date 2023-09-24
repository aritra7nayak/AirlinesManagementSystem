using Flight.Business;
using Flight.Models;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace Flight.Controllers
{
    public class AeroplaneController : Controller
    {
        private readonly AeroplaneService _aeroplaneService;

        public AeroplaneController(AeroplaneService aeroplaneService)
        {
            _aeroplaneService = aeroplaneService ?? throw new ArgumentNullException(nameof(aeroplaneService));
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10; // Adjust the page size as needed

            var aeroplanes = await _aeroplaneService.GetAeroplanesAsync();

            // Create a paged list from the aeroplanes
            IPagedList<Aeroplane> pagedList = aeroplanes.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var aeroplane = await _aeroplaneService.GetByIdAsync(id);
            if (aeroplane == null)
            {
                return NotFound();
            }
            return View(aeroplane);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Aeroplane aeroplane)
        {
            if (ModelState.IsValid)
            {
                await _aeroplaneService.AddAeroplaneAsync(aeroplane);
                return RedirectToAction(nameof(Index));
            }
            return View(aeroplane);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var aeroplane = await _aeroplaneService.GetByIdAsync(id);
            if (aeroplane == null)
            {
                return NotFound();
            }
            return View(aeroplane);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Aeroplane aeroplane)
        {
            if (id != aeroplane.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _aeroplaneService.UpdateAeroplaneAsync(aeroplane);
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aeroplane);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var aeroplane = await _aeroplaneService.GetByIdAsync(id);
            if (aeroplane == null)
            {
                return NotFound();
            }
            return View(aeroplane);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _aeroplaneService.DeleteAeroplaneAsync(id);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
