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
using cloudscribe.Pagination.Models;

namespace BikeRoom.Controllers
{
    // [Authorize(Roles = "Admin,Executive")]

    [Authorize(Roles = Constant.Admin + "," + Constant.Executive)]

    public class BikesController : Controller
    {

        private readonly AppDbContext _appDbContext;
        private readonly HostingEnvironment _hostingEnvironment;

        [BindProperty]
        public BikesViewModel bikesViewModel { get; set; }

        public BikesController(AppDbContext appDbContext, HostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
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
        [AllowAnonymous]

        public IActionResult Index( string SearchString, string SortOrder, int pageNumber = 1, int pageSize = 3)
        {
            //ViewBag.PriceSortParam=String.IsNullOrEmpty(SortOrder)? "Price_desc" : "";
            //if condition same work the above line of code

            if (String.IsNullOrEmpty(SortOrder))
                SortOrder = "Price_desc";
            else
                SortOrder = "";
            // 

            ViewBag.PriceSortParam = SortOrder;
            ViewBag.CurrentSortOrder = pageNumber;
            ViewBag.CurrentFilter = SearchString;

            //pagination logic
            var Exclude = (pageNumber * pageSize) - pageSize;
            var model = from b in _appDbContext.Bikes.Include(m => m.MakedByCompany).Include(y => y.BikesModel) select b;
            //search logic

            var modelCount = model.Count();
            if (!String.IsNullOrEmpty(SearchString))
            {
                model = model.Where(m => m.MakedByCompany.Name.Contains(SearchString));
                modelCount = model.Count();
            }

            //sort logic
            switch (SortOrder)
            {
                case "Price_desc":
                    model = model.OrderByDescending(b => b.Price);
                    break;
                default:
                    model = model.OrderBy(b => b.Price);
                    break;
            }
            // remaining part pagination
            model = model.Skip(Exclude).Take(pageSize);
            var result = new PagedResult<Bikes>
            {
                Data = model.AsNoTracking().ToList(),
                //  TotalItems = _appDbContext.Bikes.Count(),
                TotalItems = modelCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            return View(result);
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
            await UploadImage();
            await _appDbContext.SaveChangesAsync();

      

            return RedirectToAction(nameof(Index));
        }

        private async  Task UploadImage()
        {
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
        }

        [HttpGet, ActionName("Edit")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            bikesViewModel.Bikes = await _appDbContext.Bikes.SingleOrDefaultAsync(m => m.Id == id);
           
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
                return View(bikesViewModel);
            }

            _appDbContext.Bikes.Update(bikesViewModel.Bikes);
            await UploadImage();
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> View(int? id)
        {
          
            bikesViewModel.Bikes = await _appDbContext.Bikes.SingleOrDefaultAsync(m => m.Id == id);

            if (bikesViewModel.Bikes == null)
            {
                return NotFound();
            }

            return View(bikesViewModel);
        }

    }
}
