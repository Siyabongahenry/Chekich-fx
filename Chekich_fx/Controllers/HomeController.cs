using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Chekich_fx.Models;
using Chekich_fx.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Chekich_fx.Enums;
using Chekich_fx.ViewModels;
using System.Text;

namespace Chekich_fx.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this._db = db;
        }

        public async Task<IActionResult> Index(string SearchString, int? Price, Category Category = Category.All, int productPage = 1)
        {
            try
            {
                var productList = from p in _db.Shoe select p;
                if (!String.IsNullOrEmpty(SearchString))
                {
                    productList = productList.Where(p => p.Name.Contains(SearchString));
                }
                if (Category != Category.All)
                {
                    productList = productList.Where(p => p.Category == Category);
                }
                if (Price == 0)
                {
                    productList = productList.OrderBy(p => p.Price);
                }
                else if (Price == 1)
                {
                    productList = productList.OrderByDescending(p=>p.Price);
                }

                var model = new ProductFiltersViewModel<Shoe>
                {
                    ProductList = await productList.AsNoTracking().Include(p => p.Sizes.OrderBy(s=>s.Size)).ToListAsync()
                };
                StringBuilder param = new StringBuilder();
                param.Append("?productPage=:");
                var count = model.ProductList.Count;
                model.PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = 3,
                    TotalItems = count,
                    UrlParam = param.ToString()
                };
                model.ProductList = model.ProductList.Skip((productPage - 1) * model.PagingInfo.ItemsPerPage).Take(model.PagingInfo.ItemsPerPage).ToList();
                ViewBag.Category = Category;
                ViewBag.Price = Price;
                return View(model);
            }
            catch
            {
                ModelState.AddModelError("", "Having troubles with connection. " +
                "Try again, and if the problem persists " +
                "see your system administrator.");
            }
            return NotFound();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
