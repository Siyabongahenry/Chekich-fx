using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chekich_fx.Data;
using Chekich_fx.Enums;
using Chekich_fx.Models;
using Chekich_fx.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chekich_fx.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StoreController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public async Task<IActionResult> Details(int? Id,string returnUrl=null)
        {

            if (Id == null) return NotFound();
                
            var shoe = await _db.Shoe
                .Include(s=>s.Sizes)
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (shoe == null) return NotFound();
   
            ViewBag.ReturnUrl = returnUrl != null && Url.IsLocalUrl(returnUrl)? returnUrl:"/Store/Index";
           
            return View(shoe);

        }
       
       
        [HttpPost]
        public async Task<string> AddItemToCart(int? shoeId, int? size)
        {
            if (shoeId == null || size == null) return "failed";

            var userId = GetUserId();
            var cart = await _db.Cart.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
            {
               
                cart = new Cart()//create new cart with user Id to add items
                {
                    UserId = userId,
                };
                _db.Add(cart);
                await _db.SaveChangesAsync();
            }
           
            var cartItem = await _db.CartItem.FirstOrDefaultAsync(c => c.CartId == cart.Id && c.ProductId == shoeId && c.ShoeSizeInt == size);
            if (cartItem == null)
            {  
                cartItem = new CartItem()
                {
                    ProductId = (int)shoeId,
                    CartId = cart.Id,
                    Quantity = 1,
                    ShoeSizeInt = (int)size
                };
                      
            }
            else
            {
                cartItem.Quantity++;
            }
            _db.Add(cartItem);
            await _db.SaveChangesAsync();
            return "succcess";
          
        }
        public string GetUserId()
        {

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddMonths(12));
            bool IsFound = HttpContext.Request.Cookies.ContainsKey("UserId");
            if (!IsFound)
            {
                Guid Id=Guid.NewGuid();
                HttpContext.Response.Cookies.Append("UserId", Id.ToString(), cookieOptions);
                return Id.ToString();
            }
            else
            {
                string Id = Request.Cookies["UserId"];
                return Id;
            }
        }
        public IActionResult ReturnToPage(string returnUrl = "")
        {
            if(!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return NotFound();
        }

    }


}
