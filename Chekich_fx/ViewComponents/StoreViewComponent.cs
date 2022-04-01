using Chekich_fx.Data;
using Chekich_fx.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.ViewComponents
{
    public class StoreViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext db;
        public StoreViewComponent(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<Shoe> products)
        {
            if (products == null)
            {
                try
                {
                    products = await db.Shoe.Include(p=>p.Sizes).OrderBy(p=>p.Name).Take(8).ToListAsync();
                    return View(products);
                }
                catch
                {
                    ModelState.AddModelError("", "Having trouble with connections. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                }
            }
            else
            {
                return View(products);
            }
            return View(new List<Shoe>());
        }
    }
}
