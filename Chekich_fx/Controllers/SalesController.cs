using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chekich_fx.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chekich_fx.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SalesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var ProductList = await _db.Shoe
                    .AsNoTracking()
                    .Include(p=>p.Sizes).Where(p => p.DiscountPrice > 0)
                    .ToListAsync();
                return View(ProductList);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
