using AutoMapper;
using BikeRoom.DataContext;
using BikeRoom.Models;
using BikeRoom.Models.BikeModelViewModel;
using BikesRoom.Controllers.Resources;
using BikesRoom.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRoom.Controllers
{
    // [Authorize(Roles = "Admin,Executive")]

    //  [Authorize(Roles = Constant.Admin + "," + Constant.Executive)]

    public class BikesModelController : Controller
    {
        private readonly IMapper _mapper;    

        private readonly AppDbContext _appDbContext;


        [BindProperty]
        public BikeModelVM bikeModelVM { get; set; }
        public BikesModelController(AppDbContext appDbContext,IMapper mapper)
        {
            _mapper=mapper;
            _appDbContext = appDbContext;


            bikeModelVM = new BikeModelVM()
            {
                MakedByCompany = _appDbContext.MakedByCompanys.ToList(),
                BikesModel = new Models.BikesModel()
            };

        }
        public IActionResult Index()
        {
            var model = _appDbContext.BikesModels.Include(m => m.MakedByCompany);

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View(bikeModelVM);
        }


        [HttpPost, ActionName("Create")]

        public async Task<IActionResult> CreatePost()
        {

            if (!ModelState.IsValid)
            {
                bikeModelVM.MakedByCompany = _appDbContext.MakedByCompanys.ToList();

                return View(bikeModelVM);

            }

            await _appDbContext.BikesModels.AddAsync(bikeModelVM.BikesModel);
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
            bikeModelVM.BikesModel = await _appDbContext.BikesModels.Include(m => m.MakedByCompany).SingleOrDefaultAsync(m => m.Id == id);

            if (bikeModelVM.BikesModel == null)
            {
                return NotFound();
            }

            return View(bikeModelVM);
        }
        [HttpPost, ActionName("Edit")]

        public async Task<IActionResult> EditPost()
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Edit));
            }
            _appDbContext.BikesModels.Update(bikeModelVM.BikesModel);
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
            bikeModelVM.BikesModel = await _appDbContext.BikesModels.Include(m => m.MakedByCompany).SingleOrDefaultAsync(m => m.Id == id);

            if (bikeModelVM.BikesModel == null)
            {
                return NotFound();
            }

            return View(bikeModelVM);
        }
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _appDbContext.BikesModels.FindAsync(id);
            if (model != null)
            {
                _appDbContext.BikesModels.Remove(model);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        [AllowAnonymous]
        [HttpGet("Bikes/api/BikesModel/{id}")]
        public IEnumerable<BikesModel> BikesModels(int? id)
        {
            return _appDbContext.BikesModels.ToList().Where(m => m.MakedByFk == id);
        }

        [AllowAnonymous]
        [HttpGet("Bikes/api/BikesModel")]
        public IEnumerable<BikesModelResources> BikesModels()
        {
            //return _appDbContext.BikesModels.ToList();
            var bikesmodel = _appDbContext.BikesModels.ToList();
            return _mapper.Map<List<BikesModel>, List<BikesModelResources>>(bikesmodel);
            //custom mapping 
            //create mapper configuration
            //var config = new MapperConfiguration(mc => mc.CreateMap<BikesModel, BikesModelResources>());
            ////map the object
            //var mapper=new Mapper(config);

            //var BikeModelRecourse = BikesModel.Select(m => new BikesModelResources
            //{
            //    Id = m.Id,
            //    Name = m.Name,
            //}).ToList();

            // return BikesModelResource;
        }
    }
}
