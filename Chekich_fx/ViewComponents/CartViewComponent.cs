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
    public class CartViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public CartViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public string Invoke()
        {
            string UserId = HttpContext.Request.Cookies["UserId"];
            var Quantity = 0; 
            try
            {
                Cart cart =  _db.Cart.Include(c=>c.CartItems).FirstOrDefault(c=>c.UserId==UserId);
                if (cart != null)
                {
                    Quantity = cart.Items;
                }
                else
                {
                    Quantity = 0;
                }
            }
            catch
            {
                ModelState.AddModelError("", "Having trouble with connections. " +
                    "Try again, and if the problem persists " +
                     "see your system administrator.");
            }
            return Quantity.ToString();
        }
       
    }
}
