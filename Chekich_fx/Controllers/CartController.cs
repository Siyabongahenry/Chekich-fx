using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chekich_fx.Models;
using Chekich_fx.Data;
using Chekich_fx.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Chekich_fx.Utility;

namespace Chekich_fx.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            string UserId = HttpContext.Request.Cookies["UserId"];
            var cart = await _db.Cart
                .AsNoTracking()
                .Include(c=>c.CartItems)
                .ThenInclude(ci=>ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == UserId); 
            if(cart == null)
            {
                cart = new Cart();
            }
            return View(cart);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int Id,int Quantity)
        {
            var cart = await _db.CartItem.FirstOrDefaultAsync(c => c.Id == Id);

            if (cart !=null)
            {
                try
                {
                    cart.Quantity = Quantity;
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
               "Try again, and if the problem persists, " +
               "see your system administrator.");
                }

            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteProduct(int? Id)
        {
            try
            {
                if (Id != null)
                {
                    var cart = await _db.CartItem.FirstOrDefaultAsync(c => c.Id == Id);
                    if (cart != null)
                    {
                        _db.CartItem.Remove(cart);
                        await _db.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                        "see your system administrator.");
            }
            return RedirectToAction(nameof(Index));
        }
        

    }
}
