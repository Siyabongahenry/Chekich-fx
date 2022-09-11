using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chekich_fx.Data;
using Chekich_fx.Enums;
using Chekich_fx.Models;
using Chekich_fx.Utility;
using Chekich_fx.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chekich_fx.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "writepolicy")]
    [AutoValidateAntiforgeryToken]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _enviroment;

        public ProductController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IWebHostEnvironment enviroment)
        {
            _userManager = userManager;
            this._db = db;
            this._enviroment = enviroment;
        }

        public async Task<IActionResult> Index(string SearchName,string SearchDescription,int? SearchQuantity,Category category=Category.All,int productPage=1)
        {
            var products = from p in _db.Shoe select p;

            products = products.AsNoTracking().OrderBy(p => p.Name);
            if (!String.IsNullOrEmpty(SearchName))
            {
                products = products.Where(p => p.Name.Contains(SearchName));
            }
           
            if (!String.IsNullOrEmpty(SearchDescription))
            {
                products = products.AsNoTracking().Where(p => p.Description.Contains(SearchDescription));
            }
            if(SearchQuantity != null)
            {
                products = products.AsNoTracking().Where(p => p.Quantity==SearchQuantity);
            }
            if(category != Category.All)
            {
                products = products.AsNoTracking().Where(p => p.Category == category);

                List<Shoe> ListProduct = await products.ToListAsync();
            }   
            var productViewModel = new ProductFiltersViewModel<Shoe>()
            {
                ProductList = await products.Include(p=>p.Sizes).ToListAsync()
            };
            StringBuilder param = new StringBuilder();
            param.Append("?productPage=:");
            var count = productViewModel.ProductList.Count;
            productViewModel.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = 10,
                TotalItems = count,
                UrlParam = param.ToString()
            };
            productViewModel.ProductList = productViewModel.ProductList.Skip((productPage - 1) * productViewModel.PagingInfo.ItemsPerPage).Take(productViewModel.PagingInfo.ItemsPerPage).ToList();

            return View(productViewModel);
        }
        public IActionResult Create()
        {
            return View(new Shoe());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shoe product,IFormFile FormFile)
        {
            ViewBag.Name = "";
            if (FormFile == null)
            {
                ViewBag.ValidateImage = "*Select product image.";
                return View(product);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    product.ImageFileName = UploadImage(FormFile);
                    product.ApplicationUser= await _userManager.GetUserAsync(User); 
                    _db.Add(product);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), nameof(ShoeSize), new { shoeId = product.Id });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists " +
                "see your system administrator.");
            }

            return View(product);
        }
        public async Task<IActionResult> Edit(int? Id)
        {
            ViewBag.ValidateImage = "";
            if (Id == null) return NotFound();

            var product = await _db.Shoe.AsNoTracking().Include(p => p.Sizes).FirstOrDefaultAsync(p => p.Id == Id);

            if (product == null) return NotFound();
            
            return View(product);
        }
        [HttpPost, ActionName("Edit")]
        [HttpPost]
        public async Task<IActionResult> EditPost(int? Id,IFormFile FormFile)
        {
            ViewBag.ValidateImage = "";
            if (Id == null)
            {
                return NotFound();
            }

            var ProductToUpdate = await _db.Shoe.FirstOrDefaultAsync(p => p.Id == Id);

            if(FormFile != null)
            {
                DeleteImage(ProductToUpdate.ImageFileName);
                ProductToUpdate.ImageFileName = UploadImage(FormFile);
            }
            if (await TryUpdateModelAsync<Shoe>(ProductToUpdate,"", p => p.Name, p => p.Description, p => p.Price, p => p.DiscountPrice))
            {
                try
                {
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), nameof(ShoeSize), new { shoeId = ProductToUpdate.Id });
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                                "Try again, and if the problem persists " +
                                 "see your system administrator.");
                }
            }
            return View(ProductToUpdate);
        }
        public async Task<IActionResult> Details(int? Id,string returnUrl = null)
        {
            if (Id == null) return NotFound();
            
            try
            {
                var product = await _db.Shoe
                    .AsNoTracking()
                    .Include(s => s.Sizes)
                    .FirstOrDefaultAsync(p => p.Id == Id);

                if (product == null) return NotFound();
                
                if(returnUrl != null && Url.IsLocalUrl(returnUrl))
                {
                    ViewBag.ReturnUrl = returnUrl;
                }
                else
                {
                    ViewBag.ReturnUrl = "/Admin/OrderItems/Index";
                }
                return View(product);
               
            }
            catch
            {
                return NotFound();
            }    
           
        }
        public async Task<IActionResult> Delete(int? Id, bool? saveChangesError = false)
        {
            if (Id == null) return NotFound();

            //find product database
            var product = await _db.Shoe
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);

            if (product == null) return NotFound();
            

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null) return NotFound();
            
            var product = await _db.Shoe.FindAsync(Id);

            if (product == null) return RedirectToAction(nameof(Index));

            try
            { 
                _db.Shoe.Remove(product);
                await _db.SaveChangesAsync();
                DeleteImage(product.ImageFileName);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = Id, saveChangesError = true });
            }
        }
        private string UploadImage(IFormFile File)
        {

            string uploadPath = Path.Combine(_enviroment.WebRootPath, "images", "store", "product");

            return FileManager.Upload(File, uploadPath);
        }
        private void DeleteImage(string _fileName)
        {
            string imgPath = Path.Combine(_enviroment.WebRootPath, "images", "store", "product",_fileName);

            FileManager.Delete(imgPath);
        }
    }
}
