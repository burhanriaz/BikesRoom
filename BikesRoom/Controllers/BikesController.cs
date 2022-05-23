using BikeRoom.DataContext;
using BikeRoom.Models.BikeModelViewModel;
using BikesRoom.Helper;
using BikesRoom.Models.BikeModelViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Hosting.Internal;
using System.Threading.Tasks;
using BikesRoom.Models;
using System.IO;
using System;
using System.Collections.Generic;
using BikeRoom.Models;

namespace BikeRoom.Controllers
{
    // [Authorize(Roles = "Admin,Executive")]

    //[Authorize(Roles = Constant.Admin + "," + Constant.Executive)]

    public class BikesController : Controller
    {

        private readonly AppDbContext _appDbContext;
        private readonly HostingEnvironment _hostingEnvironment;



        [BindProperty]
        public BikesViewModel bikesViewModel { get; set; }

        public BikesController(AppDbContext appDbContext, HostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment= hostingEnvironment;
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
            var model = _appDbContext.Bikes.Include(m => m.MakedByCompany).Include(y => y.BikesModel);

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
                bikesViewModel.BikesModel = await _appDbContext.BikesModels.ToListAsync();
                bikesViewModel.MakedByCompany = await _appDbContext.MakedByCompanys.ToListAsync();
               // return View("Create");
                return View(bikesViewModel);
               // return RedirectToAction(nameof(Create));
            }

            await _appDbContext.Bikes.AddAsync(bikesViewModel.Bikes);
            await _appDbContext.SaveChangesAsync();

            var Id = bikesViewModel.Bikes.Id;

            var webrootpath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var SavedBike =  await _appDbContext.Bikes.FindAsync(Id);

            if(files.Count!=0)
            {
               
              
                Directory.CreateDirectory(webrootpath + @"\imgs\");
                var Imagepath =@"\imgs\";

                var Extention = Path.GetExtension(files[0].FileName);
                var RelativeImagePath = Imagepath + Id + Extention;
                var AbsPath=webrootpath+ RelativeImagePath;

                using (var stream = new FileStream(AbsPath, FileMode.Create))
                {
                   await files[0].CopyToAsync(stream);
                    ViewBag.Message = Constant.Messagesuccess;

                }
                SavedBike.ImagePath= RelativeImagePath;
               await  _appDbContext.SaveChangesAsync();
            }

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
                bikesViewModel.BikesModel = await _appDbContext.BikesModels.ToListAsync();
                bikesViewModel.MakedByCompany = await _appDbContext.MakedByCompanys.ToListAsync();
                // return View("Create");
                return View(bikesViewModel);
                // return RedirectToAction(nameof(Create));
            }

             _appDbContext.Bikes.Update(bikesViewModel.Bikes);
            await _appDbContext.SaveChangesAsync();

            var Id = bikesViewModel.Bikes.Id;

            var webrootpath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var SavedBike = await _appDbContext.Bikes.FindAsync(Id);

            if (files.Count != 0)
            {


                Directory.CreateDirectory(webrootpath + @"\imgs\");
                var Imagepath = @"\imgs\";

                var Extention = Path.GetExtension(files[0].FileName);
                var RelativeImagePath = Imagepath + Id + Extention;
                var AbsPath = webrootpath + RelativeImagePath;

                using (var stream = new FileStream(AbsPath, FileMode.Create))
                {
                    await files[0].CopyToAsync(stream);
                    ViewBag.Message = Constant.Messagesuccess;

                }
                SavedBike.ImagePath = RelativeImagePath;
                await _appDbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        //[HttpGet]
        //public IEnumerable<Bikes> Bikes(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();

        //    }
        //    bikesViewModel.Bikes = await _appDbContext.Bikes.Include(m => m.MakedByCompany).Include(m => m.BikesModel).SingleOrDefaultAsync(m => m.Id == id);

        //    if (bikesViewModel.Bikes == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bikesViewModel);
        //}
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
        //[AllowAnonymous]
        //[HttpGet("api/BikesModel/{id}")]
        //public IEnumerable<BikesModel> BikesModels(int? id)
        //{
        //    return _appDbContext.BikesModels.ToList().Where(m => m.MakedByFk == id);
        //}
    }
}
