using BikeRoom.DataContext;
using BikeRoom.Models.BikeModelViewModel;
using BikesRoom.Helper;
using BikesRoom.Models.BikeModelViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BikesRoom.Models;
namespace BikeRoom.Controllers
{
    // [Authorize(Roles = "Admin,Executive")]

    [Authorize(Roles = Constant.Admin + "," + Constant.Executive)]

    public class BikesController : Controller
    {

        private readonly AppDbContext _appDbContext;


        [BindProperty]
        public BikesViewModel bikesViewModel { get; set; }

        public BikesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            bikesViewModel = new BikesViewModel()
            {
                MakedByCompany = _appDbContext.MakedByCompanys.ToList(),
                BikesModel = _appDbContext.BikesModels.ToList(),
                Bikes = new Bikes()
            };
            //bikesViewModel = new BikesViewModel();
            //bikesViewModel.MakedByCompany = _appDbContext.MakedByCompanys.ToList();
            //bikesViewModel.BikesModel = _appDbContext.BikesModels.ToList();
            //bikesViewModel.Bikes = new Bikes();
        }

        [HttpGet]
        public IActionResult Index()
    {
        var model = _appDbContext.Bikes.Include(m => m.MakedByCompany).Include(y=>y.BikesModel);

        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {

        return View(bikesViewModel);
    }


    [HttpPost, ActionName("Create")]

    public async Task<IActionResult> CreatePost()
    {

        if (!ModelState.IsValid)
        {
            return View(nameof(Create));

        }
        await _appDbContext.Bikes.AddAsync(bikesViewModel.Bikes);
        await _appDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();

        }
            bikesViewModel.Bikes = await _appDbContext.Bikes.Include(m => m.MakedByCompany).Include(m => m.BikesModel).SingleOrDefaultAsync(m => m.Id == id);

        if (bikesViewModel.Bikes == null)
        {
            return NotFound();
        }

        return View(bikesViewModel);
    }
    [HttpPost, ActionName("Edit")]

    public async Task<IActionResult> EditPost()
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(Edit));
        }
        _appDbContext.Bikes.Update(bikesViewModel.Bikes);
        await _appDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();

        }
            bikesViewModel.Bikes = await _appDbContext.Bikes.Include(m => m.MakedByCompany).Include(m=>m.BikesModel).SingleOrDefaultAsync(m => m.Id == id);

        if (bikesViewModel.Bikes == null)
        {
            return NotFound();
        }

        return View(bikesViewModel);
    }
    [HttpPost, ActionName("Delete")]

    public async Task<IActionResult> DeletePost(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var model = await _appDbContext.Bikes.FindAsync(id);
        if (model != null)
        {
            _appDbContext.Bikes.Remove(model);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return NotFound();
    }
}
}
