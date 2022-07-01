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
          
            if (Id != null)
            {
                try
                {
                    var obj = await _db.Shoe
                        .Include(s=>s.Sizes)
                        .FirstOrDefaultAsync(s => s.Id == Id);
                    if(returnUrl != null)
                    {
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            ViewBag.ReturnUrl = returnUrl;
                        }
                        else
                        {
                            ViewBag.ReturnUrl = "/Store/Index";
                        }
                    }
                    else
                    {
                        ViewBag.ReturnUrl = "/Store/Index";
                    }
                    if (obj != null)
                    {
                        return View(obj);
                    }
                }
                catch
                {
                    return NotFound();
                }
                return NotFound();
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddProductToCart(int Id,int ShoeSize)
        {
            var userId = GetUserId();
            var cart = await _db.Cart.FirstOrDefaultAsync(c=>c.UserId==userId);
            if(cart == null)
            {
                try
                {
                    cart = new Cart()
                    {
                        UserId = userId,
                    };
                    _db.Add(cart);
                    await _db.SaveChangesAsync();
                    CartItem newCartItem = new CartItem()
                    {
                        ProductId =Id,
                        CartId = cart.Id,
                        Quantity = 1,
                        ShoeSizeInt = ShoeSize
                    };
                    _db.Add(newCartItem);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index),nameof(Cart));
                }
                catch
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                var cartItem = await _db.CartItem.FirstOrDefaultAsync(c => c.CartId == cart.Id && c.ProductId == Id && c.ShoeSizeInt==ShoeSize);
                if(cartItem == null)
                {
                    try
                    {
                        cartItem = new CartItem()
                        {
                            ProductId = Id,
                            CartId = cart.Id,
                            Quantity = 1,
                            ShoeSizeInt = ShoeSize
                        };
                        _db.Add(cartItem);
                        await _db.SaveChangesAsync();
                        return RedirectToAction(nameof(Index), nameof(Cart));
                    }
                    catch
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    try
                    {
                        cartItem.Quantity++;
                        await _db.SaveChangesAsync();
                        return RedirectToAction(nameof(Index), nameof(Cart));
                    }
                    catch
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    
                }
            }
           
        }
        [HttpPost]
        public async Task<string> AddItemToCart(int itemId, int size)
        {
            var userId = GetUserId();
            var cart = await _db.Cart.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
            {
                try
                {
                    cart = new Cart()
                    {
                        UserId = userId,
                    };
                    _db.Add(cart);
                    await _db.SaveChangesAsync();
                    CartItem newCartItem = new CartItem()
                    {
                        ProductId = itemId,
                        CartId = cart.Id,
                        Quantity = 1,
                        ShoeSizeInt = size
                    };
                    _db.Add(newCartItem);
                    await _db.SaveChangesAsync();
                    return "success";

                }
                catch
                {
                    return "error";
                }
            }
            else
            {
                var cartItem = await _db.CartItem.FirstOrDefaultAsync(c => c.CartId == cart.Id && c.ProductId == itemId && c.ShoeSizeInt == size);
                if (cartItem == null)
                {
                    try
                    {
                        cartItem = new CartItem()
                        {
                            ProductId = itemId,
                            CartId = cart.Id,
                            Quantity = 1,
                            ShoeSizeInt = size
                        };
                        _db.Add(cartItem);
                        await _db.SaveChangesAsync();
                        return "succcess";
                    }
                    catch
                    {
                        return "error";
                    }
                }
                else
                {
                    try
                    {
                        cartItem.Quantity++;
                        await _db.SaveChangesAsync();
                        return "success";
                    }
                    catch
                    {
                        return "error";
                    }

                }

            }
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
