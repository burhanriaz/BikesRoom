using BikeRoom.Models;
using Microsoft.AspNetCore.Mvc;
using BikeRoom.DataContext;
using System.Threading.Tasks;
using System.Linq;

namespace BikeRoom.Controllers
{
    public class MakedByCompanyController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public MakedByCompanyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {

            return View(_appDbContext.MakedByCompanys.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(MakedByCompany model)
        {
            if (ModelState.IsValid)
            {
                await _appDbContext.MakedByCompanys.AddAsync(model);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Data = await _appDbContext.MakedByCompanys.FindAsync(id);
            if (Data != null)
            {
                _appDbContext.MakedByCompanys.Remove(Data);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Data = await _appDbContext.MakedByCompanys.FindAsync(id);
            if (Data != null)
            {
            return View(Data);
            }
            return NotFound();

        }
        [HttpPost]
        public async Task<IActionResult> Edit(MakedByCompany model)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.MakedByCompanys.Update(model);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
